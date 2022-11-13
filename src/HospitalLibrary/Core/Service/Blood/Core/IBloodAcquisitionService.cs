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

        BloodAcquisition Get(int id);

        IEnumerable<BloodAcquisition> GetAll();

        void Create(CreateAcquisitionDTO acquisitionDTO);

        BloodAcquisition Update(BloodAcquisition bloodAcquisition);

        void Delete(BloodAcquisition bloodAcquisition);

    }
}
