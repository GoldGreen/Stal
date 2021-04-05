using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stal.Server.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeatsController : ControllerBase
    {
        private readonly StalDBContext dBContext;

        public HeatsController(StalDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var queryHeats = await dBContext.Heats
                    .Select(x => new { Heat = x, Brigade = StalDBContext.LinkedGetBrigadeWithShift(x.Date) }).ToListAsync();

            foreach (var queryItem in queryHeats)
            {
                queryItem.Heat.BrigadeNumber = queryItem.Brigade[0];
                queryItem.Heat.BrigadeShift = queryItem.Brigade[1];
            }

            return Ok(queryHeats.Select(x => x.Heat).ToList());
        }
    }
}
