using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Question04CascadedDropdown.Models
{
    public class DOBValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime dob = (DateTime)value;
                return dob.AddYears(18) < DateTime.Now;
            }
            return false;
        }
    }
}