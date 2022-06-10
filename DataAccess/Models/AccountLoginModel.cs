using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class AccountLoginModel
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

        public string? MessageDisplay { get; set; }
    }
}
