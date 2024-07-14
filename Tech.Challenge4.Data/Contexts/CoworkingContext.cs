using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Data.Configurations;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Contexts
{
    public class CoworkingContext : DbContext
    {
        public CoworkingContext(DbContextOptions<CoworkingContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Coworking> Coworking { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Reserva> Reserva { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CoworkingConfiguration());
            modelBuilder.ApplyConfiguration(new SalaConfiguration());
            modelBuilder.ApplyConfiguration(new ReservaConfiguration());
        }

    }

}
