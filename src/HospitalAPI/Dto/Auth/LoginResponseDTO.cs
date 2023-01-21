namespace HospitalAPI.Dto.Auth
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public double ExpiresIn { get; set; }
    }
}
