namespace HospitalLibrary.Core.Repository.Blood.Core
{
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodAcquisitionRepository:IBaseRepository<BloodAcquisition>
    {
        IEnumerable<BloodAcquisition> GetPendingAcquisitions();

    }
}
