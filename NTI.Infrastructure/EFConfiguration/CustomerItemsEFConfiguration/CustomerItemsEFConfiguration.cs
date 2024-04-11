using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTI.Domain.Models;

namespace NTI.Infrastructure.EFConfiguration.CustomerItemsEFConfiguration
{
    public class CustomerItemsEFConfiguration : IEntityTypeConfiguration<CustomerItem>
    {
        public void Configure(EntityTypeBuilder<CustomerItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property(x=> x.Quantity).IsRequired();
        }
    }
}