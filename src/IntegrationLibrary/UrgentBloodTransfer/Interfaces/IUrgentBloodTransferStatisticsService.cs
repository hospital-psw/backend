namespace IntegrationLibrary.UrgentBloodTransfer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public interface IUrgentBloodTransferStatisticsService
    {
        Stream GenerateHTMLReport(DateTime from, DateTime to, bool sendEmail);
        Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to);
        Dictionary<string, double> GetBloodBankShare(DateTime from, DateTime to);
        Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to);
        Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to);
    }
}
