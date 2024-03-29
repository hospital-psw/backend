﻿namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InMemoryApplicationUserRepository : IApplicationUserRepository
    {
        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationDoctor> GetAllDoctors()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationDoctor> GetAllGeneralDoctors()
        {
            ApplicationDoctor doc1 = new ApplicationDoctor("Marko", "Markovic", new DateTime(), Gender.MALE, Specialization.GENERAL, null, null);
            doc1.Id = 1;
            ApplicationDoctor doc2 = new ApplicationDoctor("Slavko", "Slavkovic", new DateTime(), Gender.MALE, Specialization.GENERAL, null, null);
            doc2.Id = 2;

            List<ApplicationDoctor> doctors = new List<ApplicationDoctor>();
            doctors.Add(doc1);
            doctors.Add(doc2);
            return doctors;
        }

        public IEnumerable<ApplicationPatient> GetAllPatients()
        {
            ApplicationDoctor doc1 = new ApplicationDoctor("Slavko", "Slavkovic", new DateTime(), Gender.MALE, Specialization.GENERAL, null, null);
            doc1.Id = 1;
            ApplicationDoctor doc2 = new ApplicationDoctor("Marko", "Markovic", new DateTime(), Gender.MALE, Specialization.GENERAL, null, null);
            doc2.Id = 2;
            ApplicationPatient pat1 = new ApplicationPatient("Mitar", "Miric", new DateTime(), Gender.MALE, doc2);
            pat1.Id = 1;
            pat1.Blocked = false;
            ApplicationPatient pat2 = new ApplicationPatient("Petar", "Petrovic", new DateTime(), Gender.MALE, doc1);
            pat2.Id = 2;
            pat2.Blocked = true;
            ApplicationPatient pat3 = new ApplicationPatient("Petar", "Pavlovic", new DateTime(), Gender.MALE, doc1);
            pat3.Id = 3;
            pat3.Blocked = true;
            ApplicationPatient pat4 = new ApplicationPatient("Mila", "Maric", new DateTime(), Gender.MALE, doc1);
            pat4.Id = 4;
            pat4.Blocked = true;
            ApplicationPatient pat5 = new ApplicationPatient("Milan", "Maric", new DateTime(), Gender.MALE, doc1);
            pat5.Id = 5;
            pat5.Blocked = false;

            List<ApplicationPatient> patients = new List<ApplicationPatient>();
            patients.Add(pat1);
            patients.Add(pat2);
            patients.Add(pat3);
            patients.Add(pat4);
            patients.Add(pat5);
            return patients;

        }

        public ApplicationPatient GetPatient(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
