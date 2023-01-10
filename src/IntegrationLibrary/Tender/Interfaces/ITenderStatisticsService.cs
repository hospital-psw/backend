namespace IntegrationLibrary.Tender.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITenderStatisticsService
    {
        string GenerateHTMLReport(DateTime from, DateTime to);
        Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to);
        Dictionary<string, double> GetBloodBankShare(DateTime from, DateTime to);
        Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to);
        Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to);
    }
}
