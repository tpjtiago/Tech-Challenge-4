using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .ToTable("Customer")
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.Email)
                .IsRequired();

            builder
                .Property(p => p.Phone)
                .IsRequired();

            builder
                .Property(p => p.Cpf)
                .IsRequired();

        }
    }
}
