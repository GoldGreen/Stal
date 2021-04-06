using Microsoft.EntityFrameworkCore;
using System;
using Stal.Shared;
using System.Linq;

namespace Stal.Server.Data
{
    public class StalDBContext : DbContext
    {
        public DbSet<Heat> Heats { get; set; }

        public StalDBContext(DbContextOptions<StalDBContext> options)
            : base(options)
        { }

        [DbFunction("GET_BRIGADE_NUMBER", "public")]
        public static int[] GetBrigadeWithShift(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
