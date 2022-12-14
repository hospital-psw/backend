namespace IntegrationServices.MonhtlyTransferService
{
    using IntegrationServices.ReportService.Model;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;

    public class MonhtlyTransferService : BackgroundService
    {
        BloodBank[] bloodBanks;
        BloodBank currentBloodBank;
        System.Timers.Timer collectTimer = new System.Timers.Timer();
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Monhtly Transfer Service worked.");
            bloodBanks = JsonConvert.DeserializeObject<BloodBank[]>(Connections.GetData("http://localhost:45488/api/BloodBank/all"));
            return base.StartAsync(cancellationToken);

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (BloodBank bank in bloodBanks)
            {
                if(bank.MonthlyTransfer!=null)
                    if (bank.MonthlyTransfer.DateTime.AddMonths(1) < DateTime.Now)
                    {
                        currentBloodBank = bank;
                        collectTimer.Elapsed += new ElapsedEventHandler(CallBloodBank);
                        collectTimer.Interval = 5000;
                        collectTimer.Enabled = true;
                    }
            }
            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void CallBloodBank(object source, ElapsedEventArgs e)
        {
            Connections.GetMonthlyBlood(currentBloodBank.MonthlyTransfer);
        }
    }
}
