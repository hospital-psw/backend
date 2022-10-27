namespace HospitalAPI.EmailServices
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task Send();
    }
}
