using System.ComponentModel.DataAnnotations;

namespace AuthAdminCrud.MVC.ViewModels
{
    public class ProfileVM
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
