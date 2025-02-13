using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Detail
    {
        [Key]
        public int Detailid { get; set; }
        [Required]
        public int VillaId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Key { get; set; }
        [Required]
        [MaxLength(500)]
        public string Value { get; set; }
        [ForeignKey("VillaId")]
        public Models.Villa? Villa { get; set; }
    }
}
