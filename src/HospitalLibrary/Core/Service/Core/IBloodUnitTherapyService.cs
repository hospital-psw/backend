namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.Therapy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodUnitTherapyService
    {
        BloodUnitTherapy Get(int id);

        BloodUnitTherapy Update(BloodUnitTherapy bloodUnitTherapy);

        IEnumerable<BloodUnitTherapy> GetAll();

        BloodUnitTherapy Add(BloodUnitTherapy bloodUnitTherapy);

        void Delete(BloodUnitTherapy bloodUnitTherapy);
    }
}
