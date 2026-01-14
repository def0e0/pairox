using Microsoft.Extensions.Logging;
using Pairox.Core.Models;
using System.Net.Http.Json;

namespace Pairox.Core.Services
{
    public class MT5Service(HttpClient httpClient, ILogger<MT5Service> logger) : IMT5Client
    {
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
