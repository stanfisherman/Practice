using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practice.Models
{
    public class Responses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResponseId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Boolean Checked { get; set; }

        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}