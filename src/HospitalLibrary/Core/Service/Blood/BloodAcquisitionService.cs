namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Service.Blood.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAcquisitionService : BaseService<BloodAcquisition>, IBloodAcquisitionService
    {


        private readonly ILogger<BloodAcquisition> _logger;

        public BloodAcquisitionService(ILogger<BloodAcquisition> logger) : base()
        {
            _logger = logger;
        }


        public override IEnumerable<BloodAcquisition> GetAll()
        {
            return base.GetAll();
        }

        public override BloodAcquisition Get(int id)
        {
            return base.Get(id);
        }

        public void Create(CreateAcquisitionDTO acquisitionDTO)
        {
            throw new NotImplementedException();
        }

        void IBloodAcquisitionService.Delete(int id)
        {
            throw new NotImplementedException();
        }

        void IBloodAcquisitionService.Update(BloodAcquisition bloodAcquisition)
        {
            throw new NotImplementedException();
        }
    }
}
