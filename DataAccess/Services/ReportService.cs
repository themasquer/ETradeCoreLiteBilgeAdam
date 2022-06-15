using AppCore.DataAccess.Models;
using DataAccess.Contexts;
using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IReportService
    {
        List<ReportModel> GetList(ReportFilterModel filter, PageModel page, OrderModel order, bool innerJoin = false);
        List<string> GetOrderExpressions();
    }

    public class ReportService : IReportService
    {
        private readonly Db _db;

        public ReportService(Db db)
        {
            _db = db;
        }

        public List<ReportModel> GetList(ReportFilterModel filter, PageModel page, OrderModel order, bool innerJoin = false)
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
                            StoreName = store.Name + " (" + (store.IsVirtual == true ? "Virtual" : "Not Virtual") + ")",
                            StoreId = store.Id
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
                            StoreName = subStore != null ? 
                                    (subStore.Name + " (" + (subStore.IsVirtual == true ? "Virtual" : "Not Virtual") + ")") 
                                : "",
                            StoreId = subStore != null ? subStore.Id : 0
                        };
            }
            switch (order.Expression)
            {
                case "Category Name":
                    query = order.IsDirectionAscending ? query.OrderBy(q => q.CategoryName) : query.OrderByDescending(q => q.CategoryName);
                    break;
                case "Product Name":
                    query = order.IsDirectionAscending ? query.OrderBy(q => q.ProductName) : query.OrderByDescending(q => q.ProductName);
                    break;
                case "Unit Price":
                    query = order.IsDirectionAscending ? query.OrderBy(q => q.UnitPrice) : query.OrderByDescending(q => q.UnitPrice);
                    break;
                case "Stock Amount":
                    query = order.IsDirectionAscending ? query.OrderBy(q => q.StockAmount) : query.OrderByDescending(q => q.StockAmount);
                    break;
                case "Expiration Date":
                    query = order.IsDirectionAscending ? query.OrderBy(q => q.ExpirationDate) : query.OrderByDescending(q => q.ExpirationDate);
                    break;
                default:
                    query = order.IsDirectionAscending ? query.OrderBy(q => q.StoreName) : query.OrderByDescending(q => q.StoreName);
                    break;
            }
            if (filter.CategoryId.HasValue)
                query = query.Where(q => q.CategoryId == filter.CategoryId);
            if (!string.IsNullOrWhiteSpace(filter.ProductName))
                query = query.Where(q => q.ProductName.ToLower().Contains(filter.ProductName.ToLower().Trim()));
            if (filter.UnitPriceMinimum != null)
                query = query.Where(q => q.UnitPrice >= filter.UnitPriceMinimum);
            if (filter.UnitPriceMaximum.HasValue)
                query = query.Where(q => q.UnitPrice <= filter.UnitPriceMaximum);
            if (filter.ExpirationDateMinimum.HasValue)
                query = query.Where(q => q.ExpirationDate >= filter.ExpirationDateMinimum);
            if (filter.ExpirationDateMaximum.HasValue)
                query = query.Where(q => q.ExpirationDate <= filter.ExpirationDateMaximum);
            if (filter.StoreIds != null && filter.StoreIds.Count > 0)
                query = query.Where(q => filter.StoreIds.Contains(q.StoreId));
            if (!string.IsNullOrWhiteSpace(filter.ProductOrStoreName))
                query = query.Where(q => q.ProductName.ToLower().Contains(filter.ProductOrStoreName.ToLower().Trim()) || q.StoreName.ToLower().Contains(filter.ProductOrStoreName.ToLower().Trim()));
            page.TotalRecordsCount = query.Count();
            query = query.Skip((page.PageNumber - 1) * page.RecordsPerPageCount).Take(page.RecordsPerPageCount);
            return query.ToList();
        }

        public List<string> GetOrderExpressions() => new List<string>() 
        {
            "Store Name", "Category Name", "Product Name", "Unit Price", "Stock Amount", "Expiration Date"
        };
    }
}
