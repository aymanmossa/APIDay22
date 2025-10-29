using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APIDay22.Core.ValidationAtributes
{
    public class OnlyLettersAttributes : ValidationAttribute
    {
        private static Regex _regex = new(@"^[A-Za-z]+$",RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string input = value.ToString()!;
            return _regex.IsMatch(input!)
                    ? ValidationResult.Success
                    : new ValidationResult("Only letters and spaces are allowed!");
        }
    }
}
