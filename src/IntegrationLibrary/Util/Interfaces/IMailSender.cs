namespace IntegrationLibrary.Util.Interfaces
{
    using System.Threading.Tasks;

    public interface IMailSender
    {

        Task RunAsync(string template, string subject, string destinationEmail);

        void SendEmail(string template, string subject, string destinationEmail);
    }
}
