namespace IntegrationLibrary.Util.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMailSender
    {
        Task RunAsync(string template);

        void SendEmail(string template);
    }
}
