namespace IntegrationServices.ReportService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Text.Json;
    using IntegrationServices.ReportService.Model;
    using Newtonsoft.Json;
    using IntegrationServices.ReportService.DTO;

    public class ReportService : BackgroundService
    {
        System.Timers.Timer collectTimer = new System.Timers.Timer();
        BloodBank[] bloodBanks;
        CalculateDTO dto;
        BloodBank currentBB;
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            /*HttpClient client = new HttpClient();
            var endpoint = new Uri("http://localhost:45488/api/BloodBank/all");
            var result = client.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json); */
            bloodBanks = JsonConvert.DeserializeObject<BloodBank[]>(Connections.GetData("http://localhost:45488/api/BloodBank/all"));
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (BloodBank bank in bloodBanks)
            {
                if (bank.DateUpdated.AddDays(bank.Frequently) < DateTime.Now && bank.Frequently != 0)
                {
                    currentBB = bank;
                    Console.WriteLine(bank.Name);
                    collectTimer.Elapsed += new ElapsedEventHandler(asdf);
                    collectTimer.Interval = 60000; //bank.Frequently*24*60*60*1000; //bank.Frequently//freqvently in miliseconds 
                    collectTimer.Enabled = true;
                }
            }

            return Task.CompletedTask;
        }
        public void asdf(object source, ElapsedEventArgs e)
        {
            var dateRange = new DateRangeDTO(currentBB.ReportFrom,currentBB.ReportTo);
            dto = JsonConvert.DeserializeObject<CalculateDTO>(Connections.PostData("http://localhost:16177/api/BloodExpenditure/calculate","{"+
                    "\"from\"" +":" + "\""+currentBB.ReportFrom.ToString()+"\"" +"," +
                    "\"to\"" + ":" + "\""+currentBB.ReportTo.ToString()+"\"" +
                    "}"));

            string fileName = GeneratePDF.GeneratePdf(currentBB.Name, dto);
            Connections.SendPDFToBB(fileName);
        }
    }
}
