using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAdminCrud.MVC.Data.Configurations
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
