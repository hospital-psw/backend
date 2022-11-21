namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatisticsService : IStatisticsService
    {
        public readonly IUnitOfWork _unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<int> GetNumberOfAppointmentsPerMonth() //TODO: unit test
        {
            try
            {
                int[] returnArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetThisYearsAppointments())
                {
                    returnArray[appointment.Date.Month - 1]++;
                }
                return returnArray;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public (IEnumerable<string>, IEnumerable<int>) GetPatientsPerDoctor()
        {
            try
            {
                Random rng = new Random();
                List<string> doctorsList = new List<string>();
                List<int> patientNumberList = new List<int>();
                foreach (ApplicationUser doctor in _unitOfWork.ApplicationUserRepository.GetAllDoctors())
                {
                    doctorsList.Add(doctor.FirstName + " " + doctor.LastName);
                    patientNumberList.Add(rng.Next(10));    // getNumberOfPatients(Doctor d)
                }
                return (doctorsList, patientNumberList);
            }
            catch (Exception)
            {
                return (null, null);
            }
        }

        public (List<int>, List<int>) GetNumberOfPatientsByAgeGroup() 
        {
            
            try
            {
                List<int> males = new List<int>();
                List<int> females = new List<int>();
                for (int i =0; i<6; i++)
                {
                    males.Add(0);
                    females.Add(0);
                }  
                foreach(ApplicationUser patient in _unitOfWork.ApplicationUserRepository.GetAllPatients())
                {
                    if(patient.Gender == Model.Enums.Gender.MALE)
                    {
                        males[GetAgeGroup(patient)]++;
                    } 
                    else {
                        females[GetAgeGroup(patient)]++;
                    }
                }
                return (males,females);
            }
            catch (Exception)
            {
                return (null, null);
            }
        }

        public int GetAgeGroup(ApplicationUser patient)
        {
            int age = GetAge(patient.DateOfBirth);
            if (age <= 15) return 0;
            if (age >= 16 && age <= 25) return 1;
            if (age >= 26 && age <= 35) return 2;
            if (age >= 36 && age <= 45) return 3;
            if (age >= 46 && age <= 60) return 4;
            return 5;
        }

        public int GetAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        public IEnumerable<ApplicationUser> Test()
        {
            return _unitOfWork.ApplicationUserRepository.GetAllDoctors();
        }
    }
}
