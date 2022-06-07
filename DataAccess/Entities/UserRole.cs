using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class UserRole
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public int? UserId { get; set; }

        public User? User { get; set; }


        [Key]
        [Column(Order = 1)]
        [Required]
        public int? RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
