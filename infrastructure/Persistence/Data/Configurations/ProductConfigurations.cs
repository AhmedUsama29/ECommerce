using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasOne(prod => prod.ProductBrand)
                    .WithMany()
                    .HasForeignKey(prod => prod.BrandId);

            builder.HasOne(prod => prod.ProductType)
                    .WithMany()
                    .HasForeignKey(prod => prod.TypeId);

            builder.Property(prod => prod.Price).HasColumnType("decimal(10,3)");


        }


    }
}
