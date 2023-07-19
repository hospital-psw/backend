namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InMemoryApplicationPatientReposiotry : IApplicationPatientRepository
    {
        public void Add(ApplicationPatient entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationPatient Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationPatient> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationPatient> GetBlocked()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationPatient> GetMalicious()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationPatient> GetNonHospitalized()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationPatient entity)
        {
            return;
        }
    }
}
