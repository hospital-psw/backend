namespace HospitalLibrary.Core.Service
{
    using HospitalAPI.Controllers.TenderStatistics;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TenderService : ITenderService
    {
        private readonly IConnections _connections;

        public TenderService(IConnections connections)
        {
            _connections = connections;
        }

        public List<double> GetMoneyPerMonth(int year)
        {
            return _connections.SendHttpRequestToIntegration(year);
        }
    }
}
