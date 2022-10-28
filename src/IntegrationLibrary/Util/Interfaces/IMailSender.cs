namespace IntegrationLibrary.Util.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMailSender
    {
        string LoadTemplate(string templatePath);

        Task RunAsync(string template, string subject, string destinationEmail);

        void SendEmail(string template, string subject, string destinationEmail);
    }
}
