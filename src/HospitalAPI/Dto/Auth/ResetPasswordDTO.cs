namespace HospitalAPI.Dto.Auth
{
    using IdentityServer4.Models;
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Password must be at least 4 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

    }
}
