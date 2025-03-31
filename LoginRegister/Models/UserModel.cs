using System.ComponentModel.DataAnnotations;

namespace LoginRegister.Models
{
    public class UserModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Username { get; set; }
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(11)]
        public string Phone { get; set; }


    }
}
