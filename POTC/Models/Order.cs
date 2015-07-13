using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POTC.Models
{
   public class Order
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "First name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
//        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country*")]
        [MaxLength(20)]
        public string Country { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Address*")]
       // [RegularExpression("^[a-zA-Z0-9_ ]*$", ErrorMessage = "*Invalid address")]
        [MaxLength(48)]
        public string Address { get; set; }

       // [Required(ErrorMessage = "Address  is required")]
        [Display(Name = "Address 2*")]
       // [RegularExpression("^[a-zA-Z0-9_ ]*$", ErrorMessage = "*")]
        [MaxLength(20)]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "City*")]
        [RegularExpression("^[a-zA-Z0-9_ ]*$", ErrorMessage = "*")]
        [MaxLength(20)]
        public string City { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "State*")]
      
        public string State { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Zip*")]
        [MaxLength(10)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email*")]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "*")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Month*")]
        public string Month { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Day*")]
        public string Day { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Year")]
        public string Year { get; set; }

       [Required(ErrorMessage = "*Gender Required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

       [Display(Name = "optin")]
       public bool optin { get; set; }
    }
}
