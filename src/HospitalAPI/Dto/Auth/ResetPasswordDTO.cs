namespace HospitalAPI.Dto.Auth
{
    using IdentityServer4.Models;
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordDTO
    {
        public string? Email { get; set; }
        public string? Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

    }
}
