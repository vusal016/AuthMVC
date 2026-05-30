using System.ComponentModel.DataAnnotations;

namespace AuthAdminCrud.MVC.ViewModels
{
    public class ProductCreateVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative.")]
        public int Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string ButtonText { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

    }
}
