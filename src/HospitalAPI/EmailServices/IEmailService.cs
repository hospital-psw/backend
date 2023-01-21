namespace HospitalAPI.EmailServices
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task Send(Appointment appointment);
        Task SendActivationEmail(string email, string token);
        Task SendPasswordResetEmail(string email, string clientURL, string token);
    }
}
