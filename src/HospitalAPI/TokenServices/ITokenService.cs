namespace HospitalAPI.TokenServices
{
    using HospitalLibrary.Core.Model.ApplicationUser;

    public interface ITokenService
    {
        string BuildToken(ApplicationUser user, string role);
        public bool IsTokenValid(string token);
    }
}
