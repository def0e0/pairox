using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Pairox.Core.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pairox.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchController(IBackgroundJobClient jobClient, ILogger<ResearchController> logger) : ControllerBase
    {
        // GET: api/<ResearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ResearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ResearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ResearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost("scan")]
        public IActionResult StartScan()
        {
            var jobId = jobClient.Enqueue<IScannerService>(service => service.RunScanAsync(CancellationToken.None));
            return Accepted(new { JobId = jobId, Message = "Scan started manually" });
        }
    }
}
