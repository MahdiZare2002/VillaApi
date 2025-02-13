
using Microsoft.EntityFrameworkCore;
using OnlineShop.Context;
using System.Linq.Expressions;

namespace OnlineShop.Services.Villa
{
    public class VillaService : IVillaService
    {
        private readonly DataContext _context;
        public VillaService(DataContext context) 
        {
            _context = context;
        }

        public async Task<Models.Villa> Create(Models.Villa villa)
        {
            _context.Villas.Add(villa);
            await _context.SaveChangesAsync();
            return villa;
        }

        public async Task<Models.Villa> Delete(Models.Villa villa)
        {
            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
            return villa;
        }

        public IQueryable<Models.Villa> GetAll(Expression<Func<Models.Villa, bool>> where = null)
        {
            var data = _context.Villas.AsQueryable();
            if (where != null)
            {
                data = data.Where(where);
            }
            return data;
        }

        public async Task<Models.Villa> GetById(int id)
        {
            var data = await _context.Villas.FindAsync(id);
            return data;
        }

        public async Task<Models.Villa> Update(Models.Villa villa)
        {
            _context.Villas.Update(villa);
            await _context.SaveChangesAsync();
            return villa;
        }
    }
}
