namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationEventService : BaseService<RenovationEvent>, IRenovationEventService
    {
        public RenovationEventService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
