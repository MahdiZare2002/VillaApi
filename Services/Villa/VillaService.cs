
using Microsoft.EntityFrameworkCore;
using OnlineShop.Context;
using OnlineShop.Models;
using OnlineShop.Pagination;
using System.Linq.Expressions;
using AutoMapper;
using OnlineShop.Dtos;

namespace OnlineShop.Services.Villa
{
    public class VillaService : IVillaService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VillaService(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<VillaPagination> SerachVillaAsync(int pageId, string filter, int take)
        {
            IQueryable<Models.Villa> result = _context.Villas.Include(x => x.Details);
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(r =>
                    r.Name.Contains(filter) ||
                    r.State.Contains(filter) ||
                    r.City.Contains(filter) ||
                    r.Address.Contains(filter));
            }
            VillaPagination pagination = new();
            pagination.Generate(result, pageId, take);
            pagination.Filter = filter;
            pagination.Villas = new List<VillaSearchDto>();
            int skip = (pageId - 1) * take;
            var list = await result.Skip(skip).Take(take).ToListAsync();

            foreach (var x in list)
            {
                pagination.Villas.Add(_mapper.Map<VillaSearchDto>(x));
            }

            return pagination;
        }

        public async Task<Models.Villa> Update(Models.Villa villa)
        {
            _context.Villas.Update(villa);
            await _context.SaveChangesAsync();
            return villa;
        }
    }
}
