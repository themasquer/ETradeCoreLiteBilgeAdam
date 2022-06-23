using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public partial class Product : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("Product Name")]
        public string? Name { get; set; }

        [DisplayName("Product Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")]
        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be {1} or positive!")]
        [DisplayName("Unit Price")]
        public double? UnitPrice { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        [Column(TypeName = "image")]
        [JsonIgnore]
        public byte[]? Image { get; set; }

        [StringLength(5)]
        [JsonIgnore]
        public string? ImageExtension { get; set; }

        [JsonIgnore]
        public List<ProductStore>? ProductStores { get; set; }
    }

    public partial class Product
    {
        [NotMapped]
        [DisplayName("Unit Price")]
        [JsonIgnore]
        public string? UnitPriceDisplay { get; set; }

        [NotMapped]
        [DisplayName("Expiration Date")]
        [JsonIgnore]
        public string? ExpirationDateDisplay { get; set; }

        [NotMapped]
        [DisplayName("Category")]
        public string? CategoryNameDisplay { get; set; }

        [NotMapped]
        [DisplayName("Stores")]
        public List<int>? StoreIds { get; set; }

        [NotMapped]
        [DisplayName("Stores")]
        public string? StoreNamesDisplay { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        [JsonIgnore]
        public string? ImageTagSrcDisplay { get; set; }
    }
}
