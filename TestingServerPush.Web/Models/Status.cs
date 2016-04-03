using System.Collections.Generic;

namespace TestingServerPush.Web.Models
{
    public class Status : BaseModel
    {
        public Status()
        {
            this.Jobs = new HashSet<Job>();
        }

        public virtual ICollection<Job> Jobs { get; set; } 
    }
}