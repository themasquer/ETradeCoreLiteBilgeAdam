﻿using DataAccess.Contexts;
using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IReportService
    {
        List<ReportModel> GetList(bool innerJoin = false);
    }

    public class ReportService : IReportService
    {
        private readonly Db _db;

        public ReportService(Db db)
        {
            _db = db;
        }

        public List<ReportModel> GetList(bool innerJoin = false)
        {
            IQueryable<ReportModel> query;
            if (innerJoin)
            {
                query = from product in _db.Products
                        join category in _db.Categories
                        on product.CategoryId equals category.Id
                        join productStore in _db.ProductStores
                        on product.Id equals productStore.ProductId 
                        join store in _db.Stores
                        on productStore.StoreId equals store.Id
                        select new ReportModel()
                        {
                            ProductId = product.Id,
                            CategoryDescription = category.Description,
                            CategoryId = category.Id,
                            CategoryName = category.Name,
                            ExpirationDate = product.ExpirationDate,
                            ExpirationDateDisplay = product.ExpirationDate.HasValue ? product.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                            ProductDescription = product.Description,
                            ProductName = product.Name,
                            StockAmount = product.StockAmount,
                            UnitPrice = product.UnitPrice,
                            UnitPriceDisplay = (product.UnitPrice ?? 0).ToString(),
                            StoreName = store.Name + " (" + (store.IsVirtual == true ? "Virtual" : "Not Virtual") + ")"
                        };
            }
            else
            {
                query = from product in _db.Products
                        join category in _db.Categories
                        on product.CategoryId equals category.Id into categories
                        from subCategory in categories.DefaultIfEmpty()
                        join productStore in _db.ProductStores
                        on product.Id equals productStore.ProductId into productStores
                        from subProductStore in productStores.DefaultIfEmpty()
                        join store in _db.Stores
                        on subProductStore.StoreId equals store.Id into stores
                        from subStore in stores.DefaultIfEmpty()
                        select new ReportModel()
                        {
                            ProductId = product.Id,
                            CategoryDescription = subCategory.Description,
                            CategoryId = subCategory.Id,
                            CategoryName = subCategory.Name,
                            ExpirationDate = product.ExpirationDate,
                            ExpirationDateDisplay = product.ExpirationDate.HasValue ? product.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                            ProductDescription = product.Description,
                            ProductName = product.Name,
                            StockAmount = product.StockAmount,
                            UnitPrice = product.UnitPrice,
                            UnitPriceDisplay = (product.UnitPrice ?? 0).ToString(),
                            StoreName = subStore.Name != null ? 
                                    (subStore.Name + " (" + (subStore.IsVirtual == true ? "Virtual" : "Not Virtual") + ")") 
                                : ""
                        };
            }
            return query.ToList();
        }
    }
}