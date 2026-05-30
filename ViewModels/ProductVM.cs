using System.ComponentModel.DataAnnotations;

namespace AuthAdminCrud.MVC.ViewModels
{
    public class ProductVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative.")]
        public int Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(50)]
        public string ButtonText { get; set; }

    }
}
