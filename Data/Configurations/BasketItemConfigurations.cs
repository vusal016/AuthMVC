using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAdminCrud.MVC.Data.Configurations
{
    public class BasketItemConfigurations : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(b => new { b.UserId, b.ProductId });
            builder.HasOne(b => b.User)
                .WithMany(u => u.BasketItems)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Product)
                .WithMany(p => p.BasketItems)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
