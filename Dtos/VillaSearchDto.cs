﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Dtos
{
    public class VillaSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public long DayPrice { get; set; }
        public long SellPrice { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public List<VillaDto> Details { get; set; }
    }
}
