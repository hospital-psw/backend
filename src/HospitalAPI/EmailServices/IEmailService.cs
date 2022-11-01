namespace HospitalAPI.EmailServices
{
    using HospitalLibrary.Core.Model;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task Send(Appointment appointment);
    }
}
