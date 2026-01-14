using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pairox.Core.Services
{
    public class ScannerService(ILogger<ScannerService> logger) : IScannerService
    {
        [DisableConcurrentExecution(timeoutInSeconds: 60 * 10)]
        public async Task RunScanAsync(CancellationToken ct)
        {
            logger.LogInformation("Starting Scan...");

            await Task.Delay(5000, ct);

            logger.LogInformation("Scan Finished.");
        }
    }
}
