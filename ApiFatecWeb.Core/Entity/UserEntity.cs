using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity
{
    [Table("user")]
    public class UserEntity
    {
        [Key]
        public int IdUser { get; set; }
        public Guid Identifier { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int IdRole{ get; set; }
        public DateTime DtUpdate { get; set; }

        [ForeignKey("idRole")]
        public virtual ICollection<RoleEntity> Role { get; set; }
    }
}
