using AppCore.DataAccess.Results.Bases;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Services.CRUD;

namespace DataAccess.Services
{
    public interface IAccountService
    {
        User Login(AccountLoginModel model);
        Result Register(AccountRegisterModel model);
    }

    public class AccountService : IAccountService
    {
        private readonly UserServiceBase _userService;

        public AccountService(UserServiceBase userService)
        {
            _userService = userService;
        }

        public User Login(AccountLoginModel model)
        {
            var user = _userService.Query().SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password && u.IsActive);
            if (user == null)
                model.MessageDisplay = "User not found!";
            return user;
        }

        public Result Register(AccountRegisterModel model)
        {
            var user = new User()
            {
                IsActive = true,
                RoleId = (int)Roles.User,
                Password = model.Password,
                UserName = model.UserName,
                UserDetail = new UserDetail()
                {
                    Address = model.Address.Trim(),
                    CityId = model.CityId,
                    CountryId = model.CountryId,
                    Email = model.Email.Trim(),
                    Sex = model.Sex
                }
            };
            return _userService.Add(user);
        }
    }
}
