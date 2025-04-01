using System.ComponentModel.DataAnnotations;

namespace LoginRegister.DTO
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
