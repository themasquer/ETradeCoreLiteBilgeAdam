using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public partial class Category : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }

        [DisplayName("Category Description")]
        [StringLength(500, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string? Description { get; set; }

        [JsonIgnore]
        public List<Product>? Products { get; set; }
    }

    public partial class Category
    {
        [NotMapped]
        [DisplayName("Products Count")]
        public int? ProductsCountDisplay { get; set; }
    }
}
