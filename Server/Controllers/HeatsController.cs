using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stal.Server.Data;
using Stal.Shared.Log;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeatsController : ControllerBase
    {
        private readonly StalDBContext dBContext;
        private readonly ILogger logger;

        public HeatsController(StalDBContext dBContext, ILogger logger)
        {
            this.dBContext = dBContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Try(async () => await dBContext.Heats.ToListAsync());
        }

        [HttpGet("brigade/{id}")]
        public async Task<IActionResult> GetBrigadeWithShift(int id)
        {
            return await Try
            (
                async () =>
                {
                    return (await dBContext.Heats
                    .Select(x => new { Heat = x, Brigade = StalDBContext.LinkedGetBrigadeWithShift(x.Date) })
                    .FirstOrDefaultAsync(x => x.Heat.Id == id)).Brigade;
                }
            );
        }

        private async Task<IActionResult> Try(Func<Task<object>> func)
        {
            try
            {
                return Ok(await func());
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
                return Problem();
            }
        }
    }
}
