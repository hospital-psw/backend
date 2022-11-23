namespace HospitalAPI.TokenServices
{
    using HospitalLibrary.Core.Model.ApplicationUser;

    public interface ITokenService
    {
        string BuildToken(ApplicationUser user, string role);
        bool IsTokenValid(string token);
        double GetExpireInDate();
    }
}
