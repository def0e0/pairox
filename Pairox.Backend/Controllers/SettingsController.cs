using Microsoft.AspNetCore.Mvc;
using Pairox.Core.Models;
using Pairox.Core.Services;

namespace Pairox.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(SettingsService? settings) : ControllerBase
    {
        // GET: api/<SettingsController>
        [HttpGet]
        public async Task<Settings?> GetAsync()
        {
            if (settings != null)
                return await settings.GetAsync(CancellationToken.None) ?? null;
            return null;
        }

        // PUT api/<SettingsController>
        [HttpPut()]
        public async Task PutAsync([FromBody] Settings value)
        {
            if (settings != null)
                await settings.UpdateAsync(value);
        }

    }
}
