using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public partial class User : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(15, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(5, ErrorMessage = "{0} must have minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must have maximum {1} characters!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Active")]
        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Roles")]
        public List<UserRole>? UserRoles { get; set; }
    }

    public partial class User
    {
        [NotMapped]
        [DisplayName("Role")]
        public string? RoleNameDisplay { get; set; }
    }
}
