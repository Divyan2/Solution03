using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Question01MVCwithServerSidePagination.Models
{
    public class DOBValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dob = (DateTime)value;
            return dob.AddYears(18) < DateTime.Now;
        }
    }
}