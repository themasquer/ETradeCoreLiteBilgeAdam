using System.ComponentModel;

namespace DataAccess.Models
{
    public class ReportFilterModel
    {
        [DisplayName("Category Name")]
        public int? CategoryId { get; set; }

        [DisplayName("Product Name")]
        public string? ProductName { get; set; }

        [DisplayName("Unit Price")]
        public double? UnitPriceMinimum { get; set; }

        public double? UnitPriceMaximum { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDateMinimum { get; set; }

        public DateTime? ExpirationDateMaximum { get; set; }

        [DisplayName("Store Name")]
        public List<int>? StoreIds { get; set; }

        public string? ProductOrStoreName { get; set; }
    }
}
