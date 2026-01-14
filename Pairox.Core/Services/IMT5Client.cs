using Pairox.Core.Models;

namespace Pairox.Core.Services
{
    public interface IMT5Client
    {
        Task<SymbolInfo> GetSymbolInfoAsync(string symbol, CancellationToken ct);
        Task SelectSymbolAsync(string symbol, bool enable, CancellationToken ct);
    }

    public record SelectSymbolDto
    {
        public bool Enable { get; set;  } = true;
    }
}
