using Hydra.Presentation.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Models
{
    public class BootcampViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [CurrentDate]
        public DateTime StartDate { get; set; }

        [Required]
        [EndDate("StartDate")]
        public DateTime? EndDate { get; set; }
        public int Progress { get; set; }

        
    }
}
