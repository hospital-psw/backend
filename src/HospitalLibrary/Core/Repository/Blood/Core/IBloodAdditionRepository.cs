namespace HospitalLibrary.Core.Repository.Blood.Core
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodAdditionRepository : IBaseRepository<BloodAddition>
    {

        public IEnumerable<BloodAddition> GetByBloodType(BloodType bloodType);
    }
}
