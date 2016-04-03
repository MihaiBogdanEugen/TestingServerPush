using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingServerPush.Web.Models
{
    public class JobPosition
    {
        public JobPosition()
        {
            this.AddedAt = DateTime.Now;
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public DateTime AddedAt { get; set; }

        [Required]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }
    }
}