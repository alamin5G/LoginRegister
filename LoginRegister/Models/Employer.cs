using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegister.Models
{
    public class Employer
    {

        [Key]
        public int EmployerID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        
        public string CompanyName { get; set; }

        
        public string CompanyDescription { get; set; }

        public string Logo { get; set; }


        public virtual User User { get; set; }
    }
}
