using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginRegister.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
