using AccommodationAssistant.Domain.Entities;
using AccommodationAssistant.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccommodationAssistant.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
