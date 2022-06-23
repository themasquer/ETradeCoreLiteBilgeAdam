using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public partial class Store : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("Store Name")]
        public string? Name { get; set; }

        [DisplayName("Virtual")]
        public bool IsVirtual { get; set; }

        [JsonIgnore]
        public List<ProductStore>? ProductStores { get; set; }
    }

    public partial class Store
    {
        [NotMapped]
        [DisplayName("Virtual")]
        public string? VirtualDisplay { get; set; }

        [NotMapped]
        [DisplayName("Products")]
        public string? ProductNamesDisplay { get; set; }
    }
}
