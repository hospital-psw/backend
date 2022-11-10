namespace HospitalLibrary.Core.Service.Blood.Core
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodAcquisitionService
    {

        public BloodAcquisition Get(int id);

        public IEnumerable<BloodAcquisition> GetAll();

        public void Create(CreateAcquisitionDTO acquisitionDTO);

        public void Update(BloodAcquisition bloodAcquisition);

        public void Delete(int id);

    }
}
