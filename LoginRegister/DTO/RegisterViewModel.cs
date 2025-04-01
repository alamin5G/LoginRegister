using System.ComponentModel.DataAnnotations;

namespace LoginRegister.DTO
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Email is mandatory")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(013|014|015|016|017|018|019)[0-9]{8}$", ErrorMessage = "Phone number must be a valid Bangladeshi number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
    }
}
