using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Configurations
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable(nameof(Sala))
                .HasKey(x => x.Id);

            builder
                .Property(s => s.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(s => s.Capacidade)
                .HasColumnType("INT")
                .IsRequired();

            builder
                .Property(s => s.PrecoHora)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
               .HasOne(s => s.Coworking)
               .WithMany(c => c.Salas)
               .HasForeignKey(s => s.CoworkingId)
               .IsRequired();
        }
    }
}
