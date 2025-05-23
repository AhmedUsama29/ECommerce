using Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            
            builder.Property(d => d.Cost)
                .HasColumnType("decimal(8,2)");
            builder.Property(d => d.Description)
                .HasColumnType("varchar(200)");
            builder.Property(d => d.DeliveryTime)
                .HasColumnType("varchar(20)");
            builder.Property(d => d.ShortName)
                .HasColumnType("varchar(10)");
        }
    }
}
