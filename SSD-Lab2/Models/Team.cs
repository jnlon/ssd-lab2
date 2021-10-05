using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// I, Jasper Hanlon, student number 000799827, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

namespace SSD_Lab2.Models
{
    public class Team
    {
        [Required]
        public string Id { get; set; }
        [Required, Display(Name = "Team Name")]
        public string TeamName { get; set; }
        [Required, Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Established Date")]
        public DateTime? EstablishedDate { get; set; }
    }
}
