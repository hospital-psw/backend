namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryDoctorRepository : IDoctorRepository
    {
        public void Add(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Doctor Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAll()
        {
            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 10, 0, 0), new DateTime(2022, 11, 10, 16, 0, 0));
            Doctor doc1 = new Doctor("Milos", "Gravara", "123", "gravara@gmail.com", Specialization.GENERAL, workingHours, null);
            doc1.Id = 1;
            Doctor doc2 = new Doctor("Vuk", "Milanovic", "123", "ckepa@gmail.com", Specialization.GENERAL, workingHours, null);
            doc2.Id = 2;
            Doctor doc3 = new Doctor("Andrija", "Stanisic", "123", "stane@gmail.com", Specialization.CARDIOLOGY, workingHours, null);
            doc3.Id = 3;
            Doctor doc4 = new Doctor("Ilija", "Galin", "123", "iki@gmail.com", Specialization.GENERAL, workingHours, null);
            doc4.Id = 4;
            Doctor doc5 = new Doctor("Nikola", "Grbovic", "123", "kwicknik@gmail.com", Specialization.CARDIOLOGY, workingHours, null);
            doc5.Id = 5;

            List<Doctor> doctors = new List<Doctor>();
            doctors.Add(doc1);
            doctors.Add(doc2);
            doctors.Add(doc3);
            doctors.Add(doc4);
            doctors.Add(doc5);

            return doctors;
        }

        public IEnumerable<Doctor> GetBySpecialization(Specialization specialization)
        {
            return GetAll().Where(x => x.Specialization == specialization);
        }

        public IEnumerable<Doctor> GetOtherSpecializationDoctors(Specialization specialization, int doctorId)
        {
            return GetBySpecialization(specialization).Where(x => x.Id != doctorId);
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
