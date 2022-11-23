namespace HospitalLibrary.Core.Service.Blood.Core
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodExpenditureService
    {

        public BloodExpenditure Get(int id);

        public IEnumerable<BloodExpenditure> GetAll();

        public void Create(CreateExpenditureDTO expendituredto);

        public void Update(CreateExpenditureDTO expendituredto);

        public BloodExpenditure Delete(int id);

        public CalculateDTO CalculateExpenditure(DateTime from, DateTime to);
    }
}
