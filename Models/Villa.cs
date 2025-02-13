using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Villa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
        [Required, MaxLength(250)]
        public string State { get; set; }
        [Required, MaxLength(255)]
        public string City { get; set; }
        [Required, MaxLength(255)]
        public string Address { get; set; }
        [Required, MaxLength(255)]
        public string Mobile { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public List<Detail>? Details { get; set; }
    }
}
