
using Microsoft.EntityFrameworkCore;
using OnlineShop.Context;
using OnlineShop.CustomerModels;
using OnlineShop.Utility;

namespace OnlineShop.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistMobile(string mobile)
        {
            return await _context.Customers.AnyAsync(x => x.Mobile.Trim() == mobile.Trim());
        }

        public async Task<bool> Register(RegisterModel model)
        {
            var hashedPassword = PasswordHelper.EncodeProSecurity(model.Password);
            Models.Customer customer = new()
            {
                Mobile = model.Mobile,
                Password = hashedPassword,
                Role = "User"
            };
            try {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { 
                return false;
            }
        }
    }
}
