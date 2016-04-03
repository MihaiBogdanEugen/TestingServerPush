using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestingServerPush.Web.Models
{
    public class Job : BaseModel
    {
        public Job()
        {
            this.Actions = new HashSet<JobAction>();
            this.Positions = new HashSet<JobPosition>();

            this.AddedAt = DateTime.Now;
            this.IsInProgress = true;
        }

        [Required]
        public DateTime AddedAt { get; set; }

        [StringLength(255)]
        public string Resolution { get; set; }

        [Required, Display(Name = "Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Required]
        public bool IsInProgress { get; set; }
        
        public virtual ICollection<JobAction> Actions { get; set; } 
        public virtual ICollection<JobPosition> Positions { get; set; } 
    }
}