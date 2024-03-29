﻿namespace HospitalLibrary.Core.Repository.Blood.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBloodAcquisitionRepository : IBaseRepository<BloodAcquisition>
    {
        IEnumerable<BloodAcquisition> GetPendingAcquisitions();
        IEnumerable<BloodAcquisition> GetAcquisitionsForSpecificDoctor(int id);
        IEnumerable<BloodAcquisition> GetAllAccepted();
        IEnumerable<BloodAcquisition> GetAllDeclined();
        IEnumerable<BloodAcquisition> GetAllPending();
        IEnumerable<BloodAcquisition> GetAllReconsidering();


    }
}
