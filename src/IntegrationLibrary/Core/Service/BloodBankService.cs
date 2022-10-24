namespace IntegrationLibrary.Core.Service
{
    using IntegrationLibrary.Core.Model;
    using IntegrationLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodBankService : BaseService<BloodBank>, IBloodBankService
    {
        public BloodBankService() : base() { }
    }
}