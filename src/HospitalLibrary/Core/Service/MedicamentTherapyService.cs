namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicamentTherapyService : BaseService<MedicamentTherapy>, IMedicamentTherapyService
    {
        public MedicamentTherapyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
