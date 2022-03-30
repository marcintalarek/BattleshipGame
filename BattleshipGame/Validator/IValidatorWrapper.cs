using System.ComponentModel.DataAnnotations;

namespace BattleshipGame.Validator
{
    public interface IValidatorWrapper
    {
        bool TryValidateObject(object instance, out ICollection<ValidationResult> validationResults);
    }
}
