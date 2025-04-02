using OnlineShop.Models;
using OnlineShop.Pagination;
using System.Linq.Expressions;

namespace OnlineShop.Services.Villa
{
    public interface IVillaService
    {
        IQueryable<Models.Villa> GetAll(Expression<Func<Models.Villa, bool>> where = null);
        Task<Models.Villa> GetById(int id);
        Task<Models.Villa> Create(Models.Villa villa);
        Task<Models.Villa> Update(Models.Villa villa);
        Task<Models.Villa> Delete(Models.Villa villa);
        Task<VillaPagination> SerachVillaAsync(int pageId, string filter, int take);
    }
}
