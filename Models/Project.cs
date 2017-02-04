using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Spatial;

namespace Practice.Models
{

    public class Projects
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        public int Time { get; set; }

        public int Incentive { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        public virtual ICollection<Responses> Responses { get; set; }
    }
}
