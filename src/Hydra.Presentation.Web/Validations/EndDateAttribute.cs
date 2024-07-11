using System.ComponentModel.DataAnnotations;

namespace Hydra.Presentation.Web.Validations
{
    public class EndDateAttribute  : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public EndDateAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if(currentValue < comparisonValue)
            {
                return new ValidationResult("End Date should be after start date");
            }
            return ValidationResult.Success;
        }
    }
}
