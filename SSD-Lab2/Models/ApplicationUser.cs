using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

// I, Jasper Hanlon, student number 000799827, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

namespace SSD_Lab2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set;  }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set;  }
        [Required, Display(Name = "User Name")]
        public override string UserName { get; set;  }
        [Required, Display(Name = "Email")]
        public override string Email { get; set;  }
        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get; set;  }
        [Display(Name = "Birth Date")]
        public string BirthDate { get; set;  }
    }
}
