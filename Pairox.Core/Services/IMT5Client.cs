using Pairox.Core.Models;

namespace Pairox.Core.Services
{
    public interface IMT5Client
    {
        Task<SymbolInfo> GetSymbolInfo(string symbol, CancellationToken ct);
    }
}
