using Microsoft.Extensions.Logging;
using Pairox.Core.Models;
using System.Net.Http.Json;

namespace Pairox.Core.Services
{
    public class MT5Service(HttpClient httpClient, ILogger<MT5Service> logger) : IMT5Client
    {
        public async Task<Rate[]> GetRatesFromPosAsync(string symbol, string timeframe, int startIndex, int count, CancellationToken ct)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<Rate[]>(
                    $"/api/v1/rates/{symbol}/{timeframe}/from-pos?start_pos={startIndex}&count={count}",
                    ct
                );

                return result ?? [];
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to fetch rates for {symbol}", symbol);
                throw;
            }
        }

        public async Task<SymbolInfo?> GetSymbolInfoAsync(string symbol, CancellationToken ct)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<SymbolInfo>(
                    $"/api/v1/symbols/{symbol}",
                    ct
                );

                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to fetch symbol info for {symbol}", symbol);
                throw;
            }
        }

        public async Task<SymbolInfo[]> GetSymbolsAsync(string[] groups, CancellationToken ct)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<SymbolInfo[]>(
                    $"/api/v1/symbols/list",
                    ct
                );

                var symbols = new List<SymbolInfo>();
                foreach (var s in result ?? []) { 
                    foreach(var g in groups) {
                        if (s.Path.StartsWith(g)) {
                            symbols.Add(s);
                            break;
                        }
                    }
                }
                return symbols.ToArray() ?? [];
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to fetch symbols");
                throw;
            }
        }

        public async Task SelectSymbolAsync(string symbol, bool enable, CancellationToken ct)
        {
            try
            {
                var result = await httpClient.PutAsJsonAsync<SelectSymbolDto>(
                    $"/api/v1/symbols/{symbol}",
                    new SelectSymbolDto { Enable = enable },
                    ct
                );
                return;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to select symbol for {symbol}", symbol);
                throw;
            }
        }
    }
}
