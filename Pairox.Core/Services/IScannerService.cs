using System;
using System.Collections.Generic;
using System.Text;

namespace Pairox.Core.Services
{
    public interface IScannerService
    {
        Task RunScanAsync(CancellationToken ct);
    }
}
