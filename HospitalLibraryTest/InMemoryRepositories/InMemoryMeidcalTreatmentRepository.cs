namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InMemoryMeidcalTreatmentRepository : IMedicalTreatmentRepository
    {
        public void Add(MedicalTreatment entity)
        {
            throw new NotImplementedException();
        }

        public MedicalTreatment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalTreatment> GetActive()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalTreatment> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalTreatment> GetDoctorsActiveTreatments(int doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalTreatment> GetDoctorsInactiveTreatments(int doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalTreatment> GetInactive()
        {
            throw new NotImplementedException();
        }

        public void Update(MedicalTreatment entity)
        {
            return;
        }
    }
}
