using System.ComponentModel.DataAnnotations;

namespace AuthAdminCrud.MVC.ViewModels
{
    public class LoginVm
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
