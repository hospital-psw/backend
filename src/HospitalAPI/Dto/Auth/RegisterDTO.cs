namespace HospitalAPI.Dto.Auth
{
    using IdentityServer4.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterDTO
    {
        [Required(ErrorMessage = "Required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Required!")]
        public bool Male{ get; set; }

        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }
    }
}
