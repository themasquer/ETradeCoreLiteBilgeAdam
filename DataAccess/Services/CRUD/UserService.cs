using AppCore.DataAccess.Services.Bases;
using AppCore.Results;
using AppCore.DataAccess.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.CRUD
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
            return base.Query().Include(u => u.Role).Include(u => u.UserDetail).Select(u => new User()
            {
                Id = u.Id,
                IsActive = u.IsActive,
                UserName = u.UserName,
                Password = u.Password,
                RoleId = u.RoleId,
                RoleNameDisplay = u.Role.Name,
                UserDetail = u.UserDetail
            });
        }

        public override Result Add(User entity, bool save = true)
        {
            if (Query().Any(q => q.UserName == entity.UserName))
                return new ErrorResult("User can not be added because user with the same name exists!");
            if (Query().Any(q => q.UserDetail.Email == entity.UserDetail.Email))
                return new ErrorResult("User can not be added because user with the same e-mail exists!");
            return base.Add(entity, save);
        }
    }
}
