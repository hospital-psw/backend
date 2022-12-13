namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryVacationRequestsRepository : IVacationRequestsRepository
    {
        public void Add(VacationRequest entity)
        {
            throw new NotImplementedException();
        }

        public VacationRequest Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VacationRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VacationRequest> GetAllApprovedByDoctorId(int doctorId)
        {
            throw new NotImplementedException();
        }

        public List<VacationRequest> GetAllDoctorId(int doctorId)
        {
            List<VacationRequest> vacationRequests = new List<VacationRequest>();

            ApplicationDoctor doc1 = new ApplicationDoctor("Marko", "Markovic", new DateTime(), Gender.MALE, Specialization.GENERAL, null, null);
            doc1.Id = 1;

            VacationRequest v1 = new VacationRequest(doc1, new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 15, 4, 0, 0), VacationRequestStatus.APPROVED, "aaa", false, "aaa");
            v1.Id = 1;
            VacationRequest v2 = new VacationRequest(doc1, new DateTime(2022, 12, 10, 4, 0, 0), new DateTime(2022, 12, 15, 4, 0, 0), VacationRequestStatus.APPROVED, "aaa", false, "aaa");
            v2.Id = 2;

            vacationRequests.Add(v1);
            vacationRequests.Add(v2);

            return vacationRequests;
        }

        public IEnumerable<VacationRequest> GetAllPending()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VacationRequest> GetAllRejectedByDoctorId(int doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VacationRequest> GetAllRequestsByDoctorsId(int doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VacationRequest> GetAllWaitingByDoctorId(int doctorId)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public void Update(VacationRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
