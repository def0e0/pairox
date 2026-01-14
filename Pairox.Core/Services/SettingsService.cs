using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Pairox.Core.Models;
using System;
using System.Text;
using System.Threading;

namespace Pairox.Core.Services
{
    public class SettingsService(HybridCache cache)
    {
        private const string SettingsCacheKey = "system:config";

        public async Task<Settings> GetAsync(CancellationToken ct)
        {
            return await cache.GetOrCreateAsync(
                key: SettingsCacheKey,
                state: SettingsCacheKey,
                factory: async (stateId, cancel) => new Settings(),
                cancellationToken: ct
            );
        }

        public async Task UpdateAsync(Settings settings)
        {
            await cache.SetAsync(SettingsCacheKey, settings);
        }
    }
}
