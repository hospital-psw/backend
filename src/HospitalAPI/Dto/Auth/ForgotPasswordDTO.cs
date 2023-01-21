namespace HospitalAPI.Dto.Auth
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string? ClientURI { get; set; }
    }
}
