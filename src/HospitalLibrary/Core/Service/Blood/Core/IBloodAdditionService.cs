namespace HospitalLibrary.Core.Service.Blood.Core
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodAdditionService
    {

        public BloodAddition Add(BloodType bloodType, int amount);

        public BloodAddition Delete(int id);

        public IEnumerable<BloodAddition> GetByBloodType(BloodType bloodType);

        public IEnumerable<BloodAddition> GetAll();
    }
}
