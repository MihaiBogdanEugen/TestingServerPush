using System.ComponentModel.DataAnnotations;

namespace TestingServerPush.Web.ViewModels
{
    public class JobNewViewModel
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}