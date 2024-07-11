using Hydra.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Models
{
    public class UserRegisterViewModel
    {
        [Required]
        [StringLength(int.MaxValue,MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [StringLength(int.MaxValue, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }

        public List<SelectListItem>? Roles { get; set; }
    }
}
