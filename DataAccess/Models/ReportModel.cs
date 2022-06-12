using System.ComponentModel;

namespace DataAccess.Models
{
    public class ReportModel
    {
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        [DisplayName("Category Name")]
        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        [DisplayName("Unit Price")]
        public string? UnitPriceDisplay { get; set; }

        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [DisplayName("Expiration Date")]
        public string? ExpirationDateDisplay { get; set; }

        [DisplayName("Store Name")]
        public string? StoreName { get; set; }



        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public double? UnitPrice { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int StoreId { get; set; }
    }
}
