namespace DataAccess.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public double UnitPrice { get; set; }
        public string ProductName { get; set; }
    }
}
