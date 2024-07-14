using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable(nameof(Reserva))
                .HasKey(r => r.Id);

            builder.Property(r => r.DataReserva)
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(r => r.HoraInicio)
                .HasColumnType("TIME")
                .IsRequired();

            builder.Property(r => r.HoraFinal)
                .HasColumnType("TIME")
                .IsRequired();

            builder.Property(r => r.StatusReserva)
                .HasConversion<int>()
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(r => r.Comparecimento)
                .HasColumnType("BIT")
                .IsRequired();

            builder.Property(r => r.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.DataPagamento)
                .HasColumnType("DATETIME");

            builder.Property(r => r.StatusPagamento)
                .HasConversion<int>()
                .HasColumnType("INT")
                .IsRequired();

            builder
               .HasOne(r => r.Customer)
               .WithMany(c => c.Reservas)
               .HasForeignKey(r => r.CustomerID)
               .IsRequired();

            builder
               .HasOne(r => r.Sala)
               .WithMany(s => s.Reservas)
               .HasForeignKey(r => r.SalaID)
               .IsRequired();
        }
    }
}
