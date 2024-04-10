using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTI.Domain.Models;

namespace NTI.Infrastructure.EFConfiguration.ItemEFConfiguration
{
    public class ItemsEFConfiguration : IEntityTypeConfiguration<Item>
    {


        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
            builder.Property(x => x.CreatedBy).HasMaxLength(50);
            builder.Property(x => x.ItemNumber).IsRequired();
            builder.Property(x => x.DefaultPrice).IsRequired();
            builder.HasMany(x => x.CustomerItems)
                .WithOne(x => x.Item)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasQueryFilter(x => !x.IsDeleted);

        }
    }
}

