using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;


namespace LoginRegister.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public string? ProfilePicture { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set; }  // JobSeeker, Employer, Admin

        [Required]
        public string Gender { get; set; }

        public virtual JobSeeker JobSeeker { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual Admin Admin { get; set; }
    }
}

