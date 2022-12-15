namespace HospitalAPI.EmailServices
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task Send(Appointment appointment);
        Task SendActivationEmail(ApplicationUser identityUser, string url);
        Task SendPasswordResetEmail(string email, string callback);
    }
}
