using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Validations
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? chosenDate = Convert.ToDateTime(value);
            if (chosenDate.Value < DateTime.Today) {
                return new ValidationResult("Invalid date");
            } else
            {
                return ValidationResult.Success;
            }
        }
    }
}
