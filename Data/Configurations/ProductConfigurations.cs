using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAdminCrud.MVC.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);
              builder.Property(p => p.Price)
                .IsRequired();
                builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(200);
                builder.Property(p => p.ButtonText)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
