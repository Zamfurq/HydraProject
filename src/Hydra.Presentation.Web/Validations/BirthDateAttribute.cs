using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Validations
{
    public class BirthDateAttribute  : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? birthDate = Convert.ToDateTime(value);
            DateTime date = DateTime.Today;
            int age = date.Year - birthDate.Value.Year;
            if(birthDate > date.AddYears(-age))
            {
                age--;
            }
            if (age < 21)
            {
                return new ValidationResult("You are not in required age");
            } else
            {
                return ValidationResult.Success;
            }

            
        }
    }
}
