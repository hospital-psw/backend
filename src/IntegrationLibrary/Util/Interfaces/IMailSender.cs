namespace IntegrationLibrary.Util.Interfaces
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IMailSender
    {

        Task RunAsync(string template, string subject, string destinationEmail, Stream attachment);
        void SendEmail(string template, string subject, string destinationEmail);
        void SendEmail(string template, string subject, string destinationEmail, Stream attachment);
    }
}
