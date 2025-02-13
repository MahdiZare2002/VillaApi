namespace OnlineShop.Services.Detail
{
    public interface IDetailService
    {
        Task<IEnumerable<Models.Detail>> GetAllVillaDetail(int id);
        Task<Models.Detail> GetDetail(int id);
        Task CreateDetail(Models.Detail detail);
        Task UpdateDetail(Models.Detail detail);
        Task DeleteDetail(Models.Detail detail);
    }
}
