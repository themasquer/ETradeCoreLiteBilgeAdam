using AppCore.DataAccess.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services
{
    public abstract class UserServiceBase : ServiceBase<User>
    {
        protected UserServiceBase(Db dbContext) : base(dbContext)
        {

        }
    }

    public class UserService : UserServiceBase
    {
        public UserService(Db dbContext) : base(dbContext)
        {

        }

        public override IQueryable<User> Query()
        {
            return base.Query().Include(u => u.Role).Select(u => new User()
            {
                Id = u.Id,
                IsActive = u.IsActive,
                UserName = u.UserName,
                Password = u.Password,
                RoleId = u.RoleId,
                RoleNameDisplay = u.Role.Name
            });
        }
    }
}
