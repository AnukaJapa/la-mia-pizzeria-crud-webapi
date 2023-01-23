using System.ComponentModel.DataAnnotations;

namespace PizzeriaDiAnaJaparidze.Validations
{
    public class Image: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            if (fieldValue.EndsWith(".jpg"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("link dovrà essere di immagine");
            }
        }
    }

    //-----------------






}
