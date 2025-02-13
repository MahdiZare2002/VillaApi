using Microsoft.EntityFrameworkCore;
using OnlineShop.Context;

namespace OnlineShop.Services.Detail
{
    public class DetailService : IDetailService
    {
        private readonly DataContext _context;
        public DetailService(DataContext context) 
        {
            _context = context;
        }

        public async Task CreateDetail(Models.Detail detail)
        {
            _context.Details.Add(detail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDetail(Models.Detail detail)
        {
            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Detail>> GetAllVillaDetail(int id)
        {
            return await _context.Details.Where(d => d.VillaId == id).ToListAsync();
        }

        public async Task<Models.Detail> GetDetail(int id)
        {
            return await _context.Details.FirstOrDefaultAsync(d => d.Detailid == id);
        }

        public async Task UpdateDetail(Models.Detail detail)
        {
            _context.Details.Update(detail);
            await _context.SaveChangesAsync();
        }
    }
}
