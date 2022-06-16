using System.ComponentModel;

namespace DataAccess.Models
{
    public class CartItemGroupByModel
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public double TotalUnitPrice { get; set; }

        [DisplayName("Product Count")]
        public int ProductCount { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }



        [DisplayName("Total Unit Price")]
        public string TotalUnitPriceDisplay { get; set; }
    }
}
