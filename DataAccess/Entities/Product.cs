using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public partial class Product : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(200)]
        [DisplayName("Product Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Unit Price")]
        public double? UnitPrice { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }

    public partial class Product
    {
        [NotMapped]
        [DisplayName("Unit Price")]
        public string? UnitPriceDisplay { get; set; }

        [NotMapped]
        [DisplayName("Expiration Date")]
        public string? ExpirationDateDisplay { get; set; }

        [NotMapped]
        [DisplayName("Category")]
        public string? CategoryNameDisplay { get; set; }
    }
}
