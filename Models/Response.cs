using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practice.Models
{
    public class Response
    {
        [Display(Name = "Response Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResponseId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Checked")]
        public Boolean Checked { get; set; }

        [Display(Name = "User Id")]
        public int? UserId { get; set; }
        [Display(Name = "Project Id")]
        public int ProjectId { get; set; }

        public  User User { get; set; }
        public  Project Project { get; set; }
    }
}