namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InMemoryApplicationPatientRepository : IApplicationPatientRepository
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

            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 10, 0, 0), new DateTime(2022, 11, 10, 16, 0, 0));
            ApplicationDoctor doc1 = new ApplicationDoctor("Milos", "Gravara", "gravara@gmail.com", Specialization.GENERAL, workingHours, null);
            doc1.Id = 1;
            ApplicationDoctor doc2 = new ApplicationDoctor("Vuk", "Milanovic", "ckepa@gmail.com", Specialization.GENERAL, workingHours, null);
            doc2.Id = 2;
            ApplicationPatient pat1 = new ApplicationPatient("Mitar", "Miric", new DateTime(), Gender.MALE, doc2);
            pat1.Id = 1;
            
            ApplicationPatient pat2 = new ApplicationPatient("Petar", "Petrovic", new DateTime(), Gender.MALE, doc2);
            pat2.Id = 2;
            ApplicationPatient pat3 = new ApplicationPatient("Petar", "Pavlovic", new DateTime(), Gender.MALE, doc2);
            pat3.Id = 3;
            ApplicationPatient pat4 = new ApplicationPatient("Mila", "Maric", new DateTime(), Gender.MALE, doc2);
            pat4.Id = 4;
            ApplicationPatient pat5 = new ApplicationPatient("Milan", "Maric", new DateTime(), Gender.MALE, doc1);
            pat5.Id = 5;

            List<ApplicationPatient> patients = new List<ApplicationPatient>();
            patients.Add(pat1);
            patients.Add(pat2);
            patients.Add(pat3);
            patients.Add(pat4);
            patients.Add(pat5);
            return patients;
        }

        public IEnumerable<ApplicationPatient> GetNonHospitalized()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationPatient entity)
        {
            throw new NotImplementedException();
        }
    }
}
