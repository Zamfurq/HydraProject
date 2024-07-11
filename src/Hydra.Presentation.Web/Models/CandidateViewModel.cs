using Hydra.Presentation.Web.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Models
{
    public class CandidateViewModel
    {

        public int? Id { get; set; }

        [Required]
        public int BootcampClassId { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string? LastName { get; set; }

        [Required]
        [BirthDate]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Domicile { get; set; } = null!;

        public List<SelectListItem>? BootcampList { get; set; }
    }
}
