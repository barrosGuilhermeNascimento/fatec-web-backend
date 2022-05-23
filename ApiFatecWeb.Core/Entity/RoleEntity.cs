using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity
{
    [Table("roles")]
    public class RoleEntity
    {
        [Key]
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
