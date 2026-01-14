using Hangfire;
using Microsoft.Extensions.Logging;
using Pairox.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pairox.Core.Services
{
    public class ScannerService(IMT5Client mt5, ILogger<ScannerService> logger) : IScannerService
    {
        private const string REDIS_KEY_PROGRESS = "research:progress";

        [DisableConcurrentExecution(timeoutInSeconds: 60 * 10)]
        public async Task RunScanAsync(CancellationToken ct)
        {
            logger.LogInformation("Starting Scan...");
            SymbolInfo[] symbols = [];
            try
            {
                symbols = await mt5.GetSymbolsAsync(["Commodities", "Forex"], ct);
                logger.LogInformation("Fetched {Count} symbols.", symbols.Length);
            }
            catch(Exception e)
            {
                logger.LogError(e, "Failed to fetch symbols");
            }

            

            foreach (var s in symbols)
            {
                try
                {
                    var rates = await mt5.GetRatesFromPosAsync(s.Name, "H1", 0, 1000, ct);
                    logger.LogInformation("Symbol: {Symbol}, Rates Fetched: {Count}", s.Name, rates.Length);
                }catch(Exception e)
                {
                    logger.LogError(e, "Failed to fetch rates");
                }
            }
            
            logger.LogInformation("Scan Finished.");
        }
    }
}
