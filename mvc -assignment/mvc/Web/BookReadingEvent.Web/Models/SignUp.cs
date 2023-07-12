using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookReadingEvent.Web.Models
{
    public class SignUp
    {
        public int UserID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "This Field is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This Field is required")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This Field is required")]
        public string EmailID { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This Field is required")]
        [StringLength(20,MinimumLength =5,ErrorMessage ="Password lenght atleast 5 character")]
        public string Password { get; set; }
        public string InvitedEvent { get; set; }

    }
}
