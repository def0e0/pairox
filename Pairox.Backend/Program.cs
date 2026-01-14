using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Caching.Hybrid;
using Pairox.Core.Services;
using Serilog;
using StackExchange.Redis;

namespace Pairox.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
            builder.Services.AddHybridCache(options =>
            {
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    // Expiration = TimeSpan.FromMinutes(5),
                    // LocalCacheExpiration = TimeSpan.FromMinutes(1)
                };
            });
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "Pairox_";
            });
            builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseRedisStorage(redisConnectionString));
            builder.Services.AddHangfireServer();

            var mt5ConnectionString = builder.Configuration.GetConnectionString("MT5") ?? "http://localhost:7000/api/v1";
            builder.Services.AddHttpClient<IMT5Client, MT5Service>(client =>
            {
                client.BaseAddress = new Uri(mt5ConnectionString);
                //client.DefaultRequestHeaders.Add("User-Agent", "MyApp-v1.0");
                client.Timeout = TimeSpan.FromSeconds(30); // Safety net
            }).AddStandardResilienceHandler();
            builder.Services.AddScoped<SettingsService>();
            builder.Services.AddScoped<PairsService>();
            builder.Services.AddScoped<IScannerService, ScannerService>();

            var app = builder.Build();
            app.UseHangfireDashboard();
            app.UseSerilogRequestLogging();
            using (var scope = app.Services.CreateScope())
            {
                var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

                recurringJobManager.AddOrUpdate<IScannerService>(
                    "scan-job",
                    service => service.RunScanAsync(CancellationToken.None),
                    Cron.Hourly(0)
                );
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
