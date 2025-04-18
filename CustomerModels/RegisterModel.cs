using System.ComponentModel.DataAnnotations;

namespace OnlineShop.CustomerModels
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }
        [Required]
        [MaxLength(90)]
        public string Password { get; set; }
    }
}
