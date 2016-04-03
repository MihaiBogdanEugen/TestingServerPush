using System;
using System.ComponentModel.DataAnnotations;

namespace TestingServerPush.Web.Models
{
    public class JobAction : BaseModel
    {
        public JobAction()
        {
            this.AddedAt = DateTime.Now;
        }

        [Required]
        public DateTime AddedAt { get; set; }

        [Required]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}