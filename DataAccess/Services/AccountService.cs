using DataAccess.Entities;
using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IAccountService
    {
        User Login(AccountModel model);
    }

    public class AccountService : IAccountService
    {
        private readonly UserServiceBase _userService;

        public AccountService(UserServiceBase userService)
        {
            _userService = userService;
        }

        public User Login(AccountModel model)
        {
            var user = _userService.Query().SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password && u.IsActive == true);
            if (user == null)
                model.MessageDisplay = "User not found!";
            return user;
        }
    }
}
