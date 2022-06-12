using AppCore.DataAccess.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Services.CRUD
{
    public abstract class CountryServiceBase : ServiceBase<Country>
    {
        protected CountryServiceBase(Db dbContext) : base(dbContext)
        {

        }
    }

    public class CountryService : CountryServiceBase
    {
        public CountryService(Db dbContext) : base(dbContext)
        {

        }
    }
}
