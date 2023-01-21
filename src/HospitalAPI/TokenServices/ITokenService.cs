namespace HospitalAPI.TokenServices
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        Task<string> BuildToken(ApplicationUser user, string role);
        bool IsTokenValid(string token);
        double GetExpireInDate();
    }
}
