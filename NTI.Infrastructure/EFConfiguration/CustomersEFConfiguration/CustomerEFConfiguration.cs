using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTI.Domain.Models;

namespace NTI.Infrastructure.EFConfiguration.CustomersEFConfiguration
{
    public class CustomerEFConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Email).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(30);
            builder.HasMany(x=> x.CustomerItems).WithOne(x=> x.Customer)
                .HasForeignKey(x=> x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasQueryFilter(x=> !x.IsDeleted);

        }
    }
}

