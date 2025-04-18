using OnlineShop.CustomerModels;

namespace OnlineShop.Services.Customer
{
    public interface ICustomerService
    {
        Task<bool> Register(RegisterModel model);
        //public LoginResultDto Login(string mobile, string pass);
        Task<bool> ExistMobile(string mobile);
        //public bool PasswordIsCorrect(string mobile, string pass);
    }
}
