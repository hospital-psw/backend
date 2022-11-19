﻿namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequest;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVacationRequestsRepository : IBaseRepository<VacationRequest>
    {
        IEnumerable<VacationRequest> GetAllApprovedByDoctorId(int doctorId);
        IEnumerable<VacationRequest> GetAllPending();
        IEnumerable<VacationRequest> GetAllRequestsByDoctorsId(int doctorId);
        IEnumerable<VacationRequest> GetAllWaitingByDoctorId(int doctorId);
        int Save();
    }
}
