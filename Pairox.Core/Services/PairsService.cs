using Pairox.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pairox.Core.Services
{
    public class PairsService(IMT5Client mt5)
    {
        public async Task CreateAsync(Pair pair)
        {

        }

        public async Task<Pair?> GetAsync(Pair pair)
        {
            return null;
        }

        public async Task<SymbolInfo> GetSymbolInfo(string symbol, CancellationToken ct)
        {
            return await mt5.GetSymbolInfoAsync(symbol, ct);
        }
    }
}
