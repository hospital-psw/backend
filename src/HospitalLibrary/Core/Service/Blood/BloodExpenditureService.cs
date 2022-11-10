namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Service.Blood.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodExpenditureService : BaseService<BloodExpenditure>, IBloodExpenditureService
    {

        private readonly ILogger<BloodExpenditure> _logger;

        public BloodExpenditureService(ILogger<BloodExpenditure> logger) : base()
        {
            _logger = logger;
        }


        public override BloodExpenditure Get(int id)
        {
            return base.Get(id);
        }

        public override IEnumerable<BloodExpenditure> GetAll()
        {
            return base.GetAll();
        }


        public void Create(CreateExpenditureDTO expendituredto)
        {
            throw new NotImplementedException();
        }

        public void Update(CreateExpenditureDTO expendituredto)
        {
            throw new NotImplementedException();
        }

        void IBloodExpenditureService.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
