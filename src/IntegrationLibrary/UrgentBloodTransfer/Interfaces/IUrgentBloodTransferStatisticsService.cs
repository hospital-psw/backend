namespace IntegrationLibrary.UrgentBloodTransfer.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IUrgentBloodTransferStatisticsService
    {        
        Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to);
        Dictionary<string, double> GetBloodBankShare(DateTime from, DateTime to);
        Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to);
        Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to);
    }
}
