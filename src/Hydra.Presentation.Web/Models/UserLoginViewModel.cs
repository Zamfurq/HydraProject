using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Models
{
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
