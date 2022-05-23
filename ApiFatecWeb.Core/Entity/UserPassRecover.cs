using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity
{
    [Table("user_pass_recover")]
    public class UserPassRecover
    {
        [Key]
        public int IdUserPassRecover { get; set; }

        public int IdUser { get; set; }

        public int RecoverNumber { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
