using AppCore.DataAccess.Services.Bases;
using AppCore.Results;
using AppCore.DataAccess.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Services.CRUD
{
    public abstract class StoreServiceBase : ServiceBase<Store>
    {
        protected StoreServiceBase(Db dbContext) : base(dbContext)
        {

        }
    }

    public class StoreService : StoreServiceBase
    {
        public StoreService(Db dbContext) : base(dbContext)
        {

        }

        public override IQueryable<Store> Query()
        {
            return base.Query().Include(s => s.ProductStores).ThenInclude(ps => ps.Product).OrderBy(s => s.Name).Select(s => new Store()
            {
                Id = s.Id,
                Name = s.Name,
                IsVirtual = s.IsVirtual,
                VirtualDisplay = s.IsVirtual ? "Yes" : "No",
                ProductStores = s.ProductStores,
                ProductNamesDisplay = string.Join("<br />", s.ProductStores.Select(ps => ps.Product.Name).ToList())
            });
        }

        public override Result Add(Store entity, bool save = true)
        {
            if (Query().Any(s => s.Name.ToLower() == entity.Name.ToLower().Trim()))
                return new ErrorResult("Store can not be added because store with the same name exists!");
            entity.Name = entity.Name.Trim();
            return base.Add(entity, save);
        }

        public override Result Update(Store entity, bool save = true)
        {
            if (Query().Any(s => s.Name.ToLower() == entity.Name.ToLower().Trim() && s.Id != entity.Id))
                return new ErrorResult("Store can not be added because store with the same name exists!");
            entity.Name = entity.Name.Trim();
            return base.Update(entity, save);
        }

        public override Result Delete(Expression<Func<Store, bool>> predicate, bool save = true)
        {
            var store = Query().SingleOrDefault(predicate);
            if (store.ProductStores != null && store.ProductStores.Count > 0)
            {
                foreach (var productStore in store.ProductStores)
                {
                    _dbContext.Set<ProductStore>().Remove(productStore);
                }
            }
            return base.Delete(predicate, save);
        }
    }
}
