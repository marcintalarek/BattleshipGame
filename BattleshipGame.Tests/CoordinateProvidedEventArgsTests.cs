using BattleshipGame.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace BattleshipGame.Tests
{
    public class CoordinateProvidedEventArgsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("x")]
        [InlineData("X")]
        [InlineData("1")]
        [InlineData("A11")]
        [InlineData("A100")]
        [InlineData("X10")]
        public void Validation_WrongInput_ReturnsError(string value)
        {
            var eventArgs = new CoordinateProvidedEventArgs(value);

            var context = new ValidationContext(eventArgs, serviceProvider: null, items: null);
            var errorResults = new List<ValidationResult>();
            bool result = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(eventArgs, context, errorResults, true);

            Assert.False(result);
            Assert.Equal("Invalid coordinate value.", errorResults.First().ErrorMessage);
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("a2")]
        [InlineData("A1")]
        [InlineData("A2")]
        [InlineData("j9")]
        [InlineData("J9")]
        [InlineData("j10")]
        [InlineData("J10")]
        public void Validation_CorrectInput_DoesNotReturnError(string value)
        {
            var eventArgs = new CoordinateProvidedEventArgs(value);

            var context = new ValidationContext(eventArgs, serviceProvider: null, items: null);
            var errorResults = new List<ValidationResult>();
            bool result = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(eventArgs, context, errorResults, true);

            Assert.True(result);
        }
    }
}
