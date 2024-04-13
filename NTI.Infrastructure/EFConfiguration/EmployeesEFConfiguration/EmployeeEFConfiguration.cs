using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTI.Domain.Models;

namespace NTI.Infrastructure.EFConfiguration.EmployeesEFConfiguration
{
    public class EmployeeEFConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=> x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x=> x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x=> x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x=> x.PasswordHash).IsRequired().HasMaxLength(800);
            builder.HasQueryFilter(x=> !x.IsDeleted);
        }
    }
}