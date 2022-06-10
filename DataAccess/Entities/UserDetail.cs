using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public partial class UserDetail
    {
        [Required]
        [Key]
        public int? UserId { get; set; }

        public User? User { get; set; }
        
        [Required]
        public Sex? Sex { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(250)]
        [DisplayName("E-Mail")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(1000)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Country")]
        public int? CountryId { get; set; }

        public Country? Country { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("City")]
        public int? CityId { get; set; }

        public City? City { get; set; }
    }

    public partial class UserDetail
    {
        [NotMapped]
        [DisplayName("Country")]
        public string? CountryNameDisplay { get; set; }

        [NotMapped]
        [DisplayName("City")]
        public string? CityNameDisplay { get; set; }
    }
}
