using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class City : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(175, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("City")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Country")]
        public int? CountryId { get; set; }

        public Country? Country { get; set; }
    }
}
