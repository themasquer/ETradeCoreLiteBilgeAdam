using AppCore.DataAccess.Services.Bases;
using AppCore.Results;
using AppCore.DataAccess.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Services.CRUD
{
    public abstract class CategoryServiceBase : ServiceBase<Category>
    {
        protected CategoryServiceBase(Db dbContext) : base(dbContext)
        {

        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> categories = await base.Query().OrderBy(c => c.Name).ToListAsync();
            return categories;
        }
    }

    public class CategoryService : CategoryServiceBase
    {
        public CategoryService(Db dbContext) : base(dbContext)
        {

        }

        public override IQueryable<Category> Query()
        {
            return base.Query().Include(c => c.Products).OrderBy(c => c.Name).Select(c => new Category()
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products,
                Description = c.Description,
                ProductsCountDisplay = c.Products.Count
            });
        }

        public override Result Add(Category entity, bool save = true)
        {
            if (Query().Any(c => c.Name.ToLower() == entity.Name.ToLower().Trim()))
                return new ErrorResult("Category can not be added because category with the same name exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();
            return base.Add(entity, save);
        }

        public override Result Update(Category entity, bool save = true)
        {
            if (Query().Any(c => c.Name.ToLower() == entity.Name.ToLower().Trim() && c.Id != entity.Id))
                return new ErrorResult("Category can not be updated because category with the same name exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();
            return base.Update(entity, save);
        }

        public override Result Delete(Expression<Func<Category, bool>> predicate, bool save = true)
        {
            var category = Query().SingleOrDefault(predicate);
            if (category.Products != null && category.Products.Count > 0)
                return new ErrorResult("Category can not be deleted because it has products!");
            return base.Delete(predicate, save);
        }
    }
}
