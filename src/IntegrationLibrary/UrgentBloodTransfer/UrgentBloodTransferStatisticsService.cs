namespace IntegrationLibrary.UrgentBloodTransfer
{
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using System;
    using System.Collections.Generic;

    public class UrgentBloodTransferStatisticsService : IUrgentBloodTransferStatisticsService
    {
        public Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> GetBloodBankShare(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
