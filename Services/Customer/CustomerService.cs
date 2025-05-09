
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Context;
using OnlineShop.CustomerModels;
using OnlineShop.Dtos;
using OnlineShop.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShop.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        public CustomerService(DataContext context, IOptions<JWTSettings> settings, IMapper mapper)
        {
            _context = context;
            _jwtSettings = settings.Value;
            _mapper = mapper;
        }

        public async Task<bool> ExistMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                throw new ArgumentException("mobile number can not be empty", nameof(mobile));
            return await _context.Customers.AnyAsync(x => x.Mobile.Trim() == mobile.Trim());
        }

        public async Task<LoginResultDto> Login(string mobile, string password)
        {
            var hashedPassword = PasswordHelper.EncodeProSecurity(password);
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Mobile.Trim() == mobile.Trim() && c.Password == hashedPassword);
            if (customer == null)
                return null;

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Role, customer.Role.ToString())
                }),
                Expires = DateTime.Now.AddDays(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            customer.JwtSecret = tokenHandler.WriteToken(token);
            return _mapper.Map<LoginResultDto>(customer);
        }

        public async Task<bool> PasswordIsCorrect(string mobile, string password)
        {
            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("password can not be empty", nameof(password));
            var hashedPassword = PasswordHelper.EncodeProSecurity(password.Trim());
            return await _context.Customers.AnyAsync(c => c.Mobile == mobile && c.Password == hashedPassword);
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
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
