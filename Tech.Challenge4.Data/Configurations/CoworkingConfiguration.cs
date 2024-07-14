using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Configurations
{
    public class CoworkingConfiguration : IEntityTypeConfiguration<Coworking>
    {
        public void Configure(EntityTypeBuilder<Coworking> builder)
        {
            builder
                .ToTable("Coworking")
                .HasKey(t => t.Id);

            builder
                .Property(c => c.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(c => c.Endereco)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(c => c.Descricao)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(c => c.HoraAbertura)
                .HasColumnType("TIME")
                .IsRequired();

            builder
                .Property(c => c.HoraFechamento)
                .HasColumnType("TIME")
                .IsRequired();
        }
    }
}
