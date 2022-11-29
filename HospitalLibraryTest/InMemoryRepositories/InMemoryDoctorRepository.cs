namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryDoctorRepository : IApplicationDoctorRepository
    {
        public void Add(ApplicationDoctor entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationDoctor Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationDoctor> GetAll()
        {
            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 10, 0, 0), new DateTime(2022, 11, 10, 16, 0, 0));
            ApplicationDoctor doc1 = new ApplicationDoctor("Milos", "Gravara", "gravara@gmail.com", Specialization.GENERAL, workingHours, null, null);
            doc1.Id = 1;
            ApplicationDoctor doc2 = new ApplicationDoctor("Vuk", "Milanovic", "ckepa@gmail.com", Specialization.GENERAL, workingHours, null, null);
            doc2.Id = 2;
            ApplicationDoctor doc3 = new ApplicationDoctor("Andrija", "Stanisic", "stane@gmail.com", Specialization.CARDIOLOGY, workingHours, null, null);
            doc3.Id = 3;
            ApplicationDoctor doc4 = new ApplicationDoctor("Ilija", "Galin", "iki@gmail.com", Specialization.GENERAL, workingHours, null, null);
            doc4.Id = 4;
            ApplicationDoctor doc5 = new ApplicationDoctor("Nikola", "Grbovic", "kwicknik@gmail.com", Specialization.CARDIOLOGY, workingHours, null, null);
            doc5.Id = 5;

            List<ApplicationDoctor> doctors = new List<ApplicationDoctor>();
            doctors.Add(doc1);
            doctors.Add(doc2);
            doctors.Add(doc3);
            doctors.Add(doc4);
            doctors.Add(doc5);

            return doctors;
        }

        public IEnumerable<ApplicationDoctor> GetBySpecialization(Specialization specialization)
        {
            return GetAll().Where(x => x.Specialization == specialization);
        }

        public IEnumerable<ApplicationDoctor> GetOtherSpecializationDoctors(Specialization specialization, int doctorId)
        {
            return GetBySpecialization(specialization).Where(x => x.Id != doctorId);
        }

        public void Update(ApplicationDoctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
