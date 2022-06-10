using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(2, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(15, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must have maximum {1} characters!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must have maximum {1} characters!")]
        [Compare("Password", ErrorMessage = "{0} and {1} must be same!")]
        [DisplayName("Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        public Sex? Sex { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(250)]
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage = "{0} is not valid!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(1000)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Country")]
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("City")]
        public int? CityId { get; set; }
    }
}
