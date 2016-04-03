using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TestingServerPush.Web.ViewModels
{
    public class JobListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resolution { get; set; }

        [Display(Name = "Added At")]
        public DateTime AddedAt { get; set; }

        [Display(Name = "Added At")]
        public string AddedAtAsText => this.AddedAt.ToString("dd/MM/yyyy HH:mm", DateTimeFormatInfo.InvariantInfo);

        [Display(Name = "Status")]
        public string StatusName { get; set; }        
    }
}