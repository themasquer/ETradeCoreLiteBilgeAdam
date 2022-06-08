using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public partial class Role : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(20, ErrorMessage = "{0} must have maximum {1} characters!")]
        [DisplayName("Role Name")]
        public string? Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
