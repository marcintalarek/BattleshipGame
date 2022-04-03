using System.ComponentModel.DataAnnotations;

namespace BattleshipGame.Validator
{
    internal interface IValidatorWrapper
    {
        bool TryValidateObject(object instance, out ICollection<ValidationResult> validationResults);
    }
}
