using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Country : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("Country")]
        public string? Name { get; set; }

        public List<City>? Cities { get; set; }
    }
}
