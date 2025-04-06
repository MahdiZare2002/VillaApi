using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }
        [Required]
        [MaxLength(90)]
        public string Password { get; set; }
        [NotMapped]
        public string JwtSecret { get; set; }
        [Required]
        [MaxLength(255)]
        public string Role { get; set; }
    }
}
