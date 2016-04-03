using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TestingServerPush.Web.ViewModels
{
    public class ActionListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Added At")]
        public DateTime AddedAt { get; set; }

        [Display(Name = "Added At")]
        public string AddedAtAsText => this.AddedAt.ToString("dd/MM/yyyy HH:mm", DateTimeFormatInfo.InvariantInfo);
    }
}
