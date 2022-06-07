using AppCore.DataAccess.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataAccess.Services
{
    public abstract class ProductServiceBase : ServiceBase<Product>
    {
        protected ProductServiceBase(Db dbContext) : base(dbContext)
        {

        }
    }

    public class ProductService : ProductServiceBase
    {
        public ProductService(Db dbContext) : base(dbContext)
        {

        }

        public override IQueryable<Product> Query()
        {
            return base.Query().Include(p => p.Category).OrderBy(p => p.Category.Name).ThenBy(p => p.Name).Select(p => new Product()
            {
                CategoryId = p.CategoryId,
                CategoryNameDisplay = p.Category.Name,
                ExpirationDate = p.ExpirationDate,
                ExpirationDateDisplay = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en-US")) : "",
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                Description = p.Description,
                StockAmount = p.StockAmount,
                UnitPriceDisplay = (p.UnitPrice ?? 0).ToString("C2", new CultureInfo("en-US"))
            });
        }

        public override Result Add(Product entity, bool save = true)
        {
            if (Query().Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim()))
                return new ErrorResult("Product can not be added because product with the same name exists!");
            return base.Add(entity, save);
        }

        public override Result Update(Product entity, bool save = true)
        {
            if (Query().Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim() && p.Id != entity.Id))
                return new ErrorResult("Product can not be updated because product with the same name exists!");
            return base.Update(entity, save);
        }
    }
}
