using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeMs.Web.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class AntiScriptAttribute : RegularExpressionAttribute
    {
        private const string pattern = @"^((?!<|>|{|}|/|&).)*$";
    
        public AntiScriptAttribute() : base(pattern)
        {
            ErrorMessage = "There is at least one invalid character.";
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage);
        }
    }
}