using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcBandas.Models
{
    public class MvcBandasContext : DbContext
    {
        public MvcBandasContext (DbContextOptions<MvcBandasContext> options)
            : base(options)
        {
        }

        public DbSet<MvcBandas.Models.Banda> Banda { get; set; }
    }
}
