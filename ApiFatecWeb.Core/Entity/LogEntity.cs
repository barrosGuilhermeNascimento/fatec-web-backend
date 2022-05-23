using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity
{
    [Table("log")]
    public class LogEntity
    {
        [Key]
        public int IdLog { get; set; }
        public int IdUser { get; set; }
        public string NmTable { get; set; }
        public string Description { get; set; }
        public DateTime DtUpdate { get; set; }

        [ForeignKey("idUser")]
        public virtual ICollection<UserEntity> User { get; set; }
    }
}
