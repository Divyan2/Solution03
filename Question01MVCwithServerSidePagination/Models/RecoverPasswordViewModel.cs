using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Question01MVCwithServerSidePagination.Models
{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including uppercase, lowercase, and a number")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Security question is required")]
        public int SecurityCode { get; set; }

        [Required(ErrorMessage = "Security answer is required")]
        public string SecurityAnswer { get; set; }
    }
}