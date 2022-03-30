using System.ComponentModel.DataAnnotations;

namespace BattleshipGame.Validator
{
    public class ValidatorWrapper : IValidatorWrapper
    {
        public bool TryValidateObject(object instance, out ICollection<ValidationResult> validationResults)
        {
            var context = new ValidationContext(instance, serviceProvider: null, items: null);
            validationResults = new List<ValidationResult>();
            return System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, context, validationResults, true);
        }
    }
}
