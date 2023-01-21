namespace IntegrationAPI.DTO.BloodBank
{
    using System;

    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
