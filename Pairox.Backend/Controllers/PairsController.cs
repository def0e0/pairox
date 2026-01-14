using Microsoft.AspNetCore.Mvc;
using Pairox.Core.Models;
using Pairox.Core.Services;

namespace Pairox.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairsController(PairsService pairs) : ControllerBase
    {
        // GET: api/<PairsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PairsController>/5
        [HttpGet("{symbol}")]
        public async Task<SymbolInfo> GetAsync(string symbol)
        {
            return await pairs.GetSymbolInfo(symbol, new CancellationToken());
        }

        // POST api/<PairsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PairsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PairsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
