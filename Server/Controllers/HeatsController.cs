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
            return await Try(async () => Ok(await dBContext.Heats.ToListAsync()));
        }

        [HttpGet("brigade/{ticks}")]
        public async Task<IActionResult> GetBrigadeWithShift(long ticks)
        {
            return await Try
            (
                async () =>
                {
                    return Ok(await dBContext.Heats
                                .Select(x => StalDBContext.LinkedGetBrigadeWithShift(new DateTime(ticks)))
                                .FirstOrDefaultAsync());
                }
            );
        }

        private async Task<IActionResult> Try(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
                return Problem();
            }
        }
    }
}
