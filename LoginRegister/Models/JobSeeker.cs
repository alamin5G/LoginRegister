using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegister.Models
{
    public class JobSeeker
    {
        [Key]
        public int JobSeekerID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        
        public string Resume { get; set; }

        
        public string CoverLetter { get; set; }

        
        public string PreferredJobTitle { get; set; }

        
        public string Location { get; set; }

        public virtual User User { get; set; }
    }
}
