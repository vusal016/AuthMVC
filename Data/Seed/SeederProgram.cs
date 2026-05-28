namespace AuthAdminCrud.MVC.Data.Seed
{
    public static class SeederProgram
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            await RoleSeed.SeedRoles(roleManager);
            await RoleSeed.SeedAdminUser(userManager);

        }
    }
}