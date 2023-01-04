namespace HospitalAPI.Controllers.TenderStatistics
{
    using System.Collections.Generic;

    public interface IConnections
    {
        public List<double> SendHttpRequestToIntegration(int year);
        public List<double> SendHttpRequestToIntegrationBloodQuantity(int year, int bloodType);
    }
}
