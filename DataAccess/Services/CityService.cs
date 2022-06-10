using AppCore.DataAccess.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Services
{
    public abstract class CityServiceBase : ServiceBase<City>
    {
        protected CityServiceBase(Db dbContext) : base(dbContext)
        {

        }

        public List<City> GetCities(int countryId)
        {
            return Query().Where(c => c.CountryId == countryId).ToList();
        }
    }

    public class CityService : CityServiceBase
    {
        public CityService(Db dbContext) : base(dbContext)
        {

        }
    }
}
