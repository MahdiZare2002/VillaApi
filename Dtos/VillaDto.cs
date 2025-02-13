using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Dtos
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "نام ویلا نباید بیش از 255 حرف باشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = "استان ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "استان ویلا نباید بیش از 255 حرف باشد")]
        public string State { get; set; }
        [Required(ErrorMessage = "شهرستان ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "شهرستان ویلا نباید بیش از 255 حرف باشد")]
        public string City { get; set; }
        [Required(ErrorMessage = "ادرس کامل ویلا اجباری است")]
        [MaxLength(500, ErrorMessage = "آدرس کامل ویلا نباید بیش از 500 حرف باشد")]
        public string Address { get; set; }
        [Required(ErrorMessage = "شماره تماس کامل ویلا اجباری است")]
        [MaxLength(11, ErrorMessage = "شماره تماس ویلا نباید بیش از 11 حرف باشد")]
        [MinLength(11, ErrorMessage = "شماره تماس ویلا نباید کمتر از 11 حرف باشد")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "قیمت کرایه روزانه ویلا اجباری است")]
        public long DayPrice { get; set; }
        [Required(ErrorMessage = "قیمت فروش  ویلا اجباری است")]
        public long SellPrice { get; set; }
    }
}
