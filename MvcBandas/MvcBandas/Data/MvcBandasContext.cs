using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace MvcBandas.Models
{
    public class MvcBandasContext : DbContext
    {
        public MvcBandasContext (DbContextOptions<MvcBandasContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Banda> Bandas { get; set; }
        public DbSet<Concierto> Conciertos { get; set; }
    }
}
