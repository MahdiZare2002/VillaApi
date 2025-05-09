using OnlineShop.CustomerModels;
using OnlineShop.Dtos;

namespace OnlineShop.Services.Customer
{
    public interface ICustomerService
    {
        Task<bool> Register(RegisterModel model);
        Task<LoginResultDto> Login(string mobile, string pass);
        Task<bool> ExistMobile(string mobile);
        Task<bool> PasswordIsCorrect(string mobile, string pass);
    }
}
