namespace AuthAdminCrud.MVC.Models
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}