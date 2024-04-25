using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TASKTRACKERSYSTEM.CustomValidation
{
    public class AlphanumericAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();

                
                if (!Regex.IsMatch(stringValue, "^[a-zA-Z0-9]*$") || stringValue.Length > 50)
                {
                    return new ValidationResult("Task Name should allow only alphanumeric characters and be maximum 50 characters.");
                }
            }
            return ValidationResult.Success;
        }
    }
}