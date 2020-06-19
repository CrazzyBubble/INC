using System;
using System.ComponentModel.DataAnnotations;

namespace INCWebServer.Models
{
    public class RegistrationModel
    {

        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Firstname is not specified")]
        public string Firstname { get; set; }

        public string Lastname { get; set; } = "";
        
        [Required(ErrorMessage = "Birthday is not specified")]
        public DateTime Birthday { get; set; }
    }
}
