namespace HospitalLibrary.Core.Service.Blood.Core
{

    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodUnitService
    {
        BloodUnit Add(BloodUnit bloodUnit);

        bool Delete(int id);

        IEnumerable<BloodUnit> GetAll();

        BloodUnit Get(int id);
        BloodUnit Update(BloodUnit bloodUnit);

        int GetAmountForSpecificBloodType(BloodType bloodType);
        BloodUnit GetByBloodType(BloodType bloodType);
    }
}
