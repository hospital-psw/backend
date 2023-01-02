namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Util;
    using Syncfusion.Pdf.Lists;
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

        public IEnumerable<int> GetNumberOfAppointmentsPerMonth()
        {
            try
            {
                List<int> retrunList = ListFactory.CreateList<int>(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetThisYearsAppointments())
                {
                    retrunList[appointment.Date.Month - 1]++;
                }
                return retrunList;
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
                List<string> doctorsList = new List<string>();
                List<int> patientNumberList = new List<int>();
                List<ApplicationDoctor> doctorList = _unitOfWork.ApplicationDoctorRepository.GetAll().ToList(); //had to do it like this
                foreach (ApplicationDoctor doctor in doctorList)
                {
                    doctorsList.Add(doctor.FirstName + " " + doctor.LastName);
                    patientNumberList.Add(GetNumberOfDoctorsPatients(doctor));  //because this function iterates over the same collection
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
                List<int> males = ListFactory.CreateList(0, 0, 0, 0, 0, 0);
                List<int> females = ListFactory.CreateList(0, 0, 0, 0, 0, 0);
                foreach (ApplicationPatient patient in _unitOfWork.ApplicationUserRepository.GetAllPatients())
                {
                    if (patient.Gender == Model.Enums.Gender.MALE)
                    {
                        males[GetAgeGroup(patient)]++;
                    }
                    else
                    {
                        females[GetAgeGroup(patient)]++;
                    }
                }
                return (males, females);
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

        public List<int> GetUsersByType()
        {
            List<int> retList = ListFactory.CreateList(0, 0, 0, 0);
            retList[0] = _unitOfWork.ApplicationPatientRepository.GetAll().Count();
            foreach (ApplicationDoctor doctor in _unitOfWork.ApplicationUserRepository.GetAllDoctors())
            {
                if (doctor.Specialization == Model.Enums.Specialization.GENERAL) retList[1]++;
                if (doctor.Specialization == Model.Enums.Specialization.NEUROLOGY) retList[2]++;
                if (doctor.Specialization == Model.Enums.Specialization.CARDIOLOGY) retList[3]++;
            }
            return retList;
        }

        public int GetNumberOfDoctorsPatients(ApplicationDoctor doctor)
        {
            int counter = 0;
            foreach (ApplicationPatient patient in _unitOfWork.ApplicationPatientRepository.GetAll())
            {
                if (patient.applicationDoctor == doctor) counter++;
            }
            return counter;
        }
        public List<int> GetNumberOfVacationDaysPerMonth(int doctorId)
        {
            try
            {
                List<int> retrunList = ListFactory.CreateList<int>(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                foreach (VacationRequest v in _unitOfWork.VacationRequestsRepository.GetAllDoctorId(doctorId))
                {
                    int numberOfDays = (v.To - v.From).Days;
                    retrunList[v.From.Month - 1] = retrunList[v.From.Month - 1] + numberOfDays;
                }
                return retrunList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<int> GetNumberOfDoctorAppointmentsPerYear(int doctorId, int year)
        {
            try
            {
                List<int> retList = ListFactory.CreateList<int>(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetYearlyAppointmentsForDoctor(doctorId, year))
                {
                    retList[appointment.Date.Month - 1] = retList[appointment.Date.Month - 1] + 1;
                }

                return retList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<int> GetNumberOfDoctorAppointmentsPerMonth(int doctorId, int month, int year)
        {
            try
            {
                List<int> retList = CreateMonthList(month);
                Console.WriteLine(retList.Count);
                List<Appointment> appointments = _unitOfWork.AppointmentRepository.GetMonthlyAppointmentsForDoctor(doctorId, year, month).ToList();
                foreach (Appointment appointment in appointments)
                {
                    Console.WriteLine(appointment.Date.Day);
                    retList[appointment.Date.Day - 1] = retList[appointment.Date.Day - 1] + 1;
                }
                Console.WriteLine("zavrsio", retList.Count);
                return retList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private List<int> CreateMonthList(int month)
        {
            switch (month)
            {
                case 1: return Enumerable.Repeat(0, 31).ToList();
                case 2: return Enumerable.Repeat(0, 28).ToList();
                case 3: return Enumerable.Repeat(0, 31).ToList();
                case 4: return Enumerable.Repeat(0, 30).ToList();
                case 5: return Enumerable.Repeat(0, 31).ToList();
                case 6: return Enumerable.Repeat(0, 30).ToList();
                case 7: return Enumerable.Repeat(0, 31).ToList();
                case 8: return Enumerable.Repeat(0, 31).ToList();
                case 9: return Enumerable.Repeat(0, 30).ToList();
                case 10: return Enumerable.Repeat(0, 31).ToList();
                case 11: return Enumerable.Repeat(0, 30).ToList();
                case 12: return Enumerable.Repeat(0, 31).ToList();
            }
            return null;
        }
    }
}
