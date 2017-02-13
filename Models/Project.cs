using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Spatial;

namespace Practice.Models
{

    public class Project
    {
        [Display(Name = "Project Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Project Length")]
        public int Time { get; set; }

        [Display(Name = "Incentive")]
        public int Incentive { get; set; }

        [Display(Name = "Notes")]
        [Column(TypeName = "text")]
        public string Notes { get; set; }

        public ICollection<Response> Response { get; set; }
    }
}
