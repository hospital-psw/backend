namespace HospitalLibrary.Core.Service.Blood.Core
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
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

        IEnumerable<BloodAcquisition> GetPendingAcquisitions();

        BloodAcquisition DeclineAcquisition(int id);

        BloodAcquisition AcceptAcquisition(int id);

        IEnumerable<BloodAcquisition> GetAcquisitionsForSpecificDoctor(int id);
        IEnumerable<BloodAcquisition> GetAllAcceptedAcquisition();
        IEnumerable<BloodAcquisition> GetAllDeclinedAcquisition();
        IEnumerable<BloodAcquisition> GetAllPendingAcquisition();
        IEnumerable<BloodAcquisition> GetAllReconsideringAcquisition();
        void HandleBloodRequest(BloodRequestStatus status, int id, string managerComment);


    }
}
