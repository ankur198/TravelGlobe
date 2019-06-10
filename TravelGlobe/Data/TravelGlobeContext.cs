using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelGlobe.Model;

namespace TravelGlobe.Models
{
    public class TravelGlobeContext : DbContext
    {
        public TravelGlobeContext(DbContextOptions<TravelGlobeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Position>()
                .Property(e => e.position)
                .HasConversion<string>();


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ikigai> Ikigai { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Activity> Activity { get; set; }
    }
}
