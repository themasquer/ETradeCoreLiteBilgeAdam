﻿using AppCore.DataAccess.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace DataAccess.Services.CRUD
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
            return base.Query().Include(p => p.Category).Include(p => p.ProductStores).OrderBy(p => p.Category.Name).ThenBy(p => p.Name).Select(p => new Product()
            {
                CategoryId = p.CategoryId,
                CategoryNameDisplay = p.Category.Name,
                ExpirationDate = p.ExpirationDate,
                ExpirationDateDisplay = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                Description = p.Description,
                StockAmount = p.StockAmount,
                UnitPriceDisplay = (p.UnitPrice ?? 0).ToString("C2"),
                ProductStores = p.ProductStores,
                StoreNamesDisplay = string.Join("<br />", p.ProductStores.Select(ps => ps.Store.Name + " (" + (ps.Store.IsVirtual ? "Virtual" : "Not Virtual") + ")").ToList()),
                StoreIds = p.ProductStores.Select(ps => ps.StoreId).ToList()
            });
        }

        public override Result Add(Product entity, bool save = true)
        {
            if (Query().Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim()))
                return new ErrorResult("Product can not be added because product with the same name exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();
            entity.ProductStores = entity.StoreIds?.Select(sId => new ProductStore()
            {
                StoreId = sId
            }).ToList();
            return base.Add(entity, save);
        }

        public override Result Update(Product entity, bool save = true)
        {
            if (Query().Any(p => p.Name.ToLower() == entity.Name.ToLower().Trim() && p.Id != entity.Id))
                return new ErrorResult("Product can not be updated because product with the same name exists!");
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description?.Trim();
            var product = Query().SingleOrDefault(p => p.Id == entity.Id);
            if (product.ProductStores != null && product.ProductStores.Count > 0)
            {
                foreach (var productStore in product.ProductStores)
                {
                    _dbContext.Set<ProductStore>().Remove(productStore);
                }
            }
            entity.ProductStores = entity.StoreIds?.Select(sId => new ProductStore()
            {
                StoreId = sId
            }).ToList();
            return base.Update(entity, save);
        }

        public override Result Delete(Expression<Func<Product, bool>> predicate, bool save = true)
        {
            var product = Query().SingleOrDefault(predicate);
            if (product.ProductStores != null && product.ProductStores.Count > 0)
            {
                foreach (var productStore in product.ProductStores)
                {
                    _dbContext.Set<ProductStore>().Remove(productStore);
                }
            }
            return base.Delete(predicate, save);
        }
    }
}