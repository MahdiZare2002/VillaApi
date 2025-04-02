using OnlineShop.Dtos;

namespace OnlineShop.Pagination
{
    public class VillaPagination : BasePagination
    {
        public List<VillaSearchDto> Villas { get; set; }
        public string Filter { get; set; }
    }
}
