using Pairox.Core.Models;

namespace Pairox.Core.Services
{
    public interface IMT5Client
    {
        Task<SymbolInfo[]> GetSymbolsAsync(string[] groups, CancellationToken ct);
        Task<SymbolInfo> GetSymbolInfoAsync(string symbol, CancellationToken ct);
        Task SelectSymbolAsync(string symbol, bool enable, CancellationToken ct);
        Task<Rate[]> GetRatesFromPosAsync(string symbol, string timeframe, int startIndex, int count, CancellationToken ct);
    }

    public record SelectSymbolDto
    {
        public bool Enable { get; set;  } = true;
    }
}
