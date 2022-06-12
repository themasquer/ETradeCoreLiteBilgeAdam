using AppCore.DataAccess.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Services.CRUD
{
    public abstract class CityServiceBase : ServiceBase<City>
    {
        protected CityServiceBase(Db dbContext) : base(dbContext)
        {

        }
    }

    public class CityService : CityServiceBase
    {
        public CityService(Db dbContext) : base(dbContext)
        {

        }
    }
}
