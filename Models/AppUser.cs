namespace AuthAdminCrud.MVC.Models
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}