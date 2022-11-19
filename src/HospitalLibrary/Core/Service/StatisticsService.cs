namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
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
        public IEnumerable<string> getDoctorNames()
        {
            try
            {
                List<string> returnList = new List<string>();
                foreach (User doctor in _unitOfWork.DoctorRepository.GetAll())
                {
                    returnList.Add(doctor.FirstName + " " + doctor.LastName);
                }
                return returnList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int getNumberOfDoctorsPatients() //TODO: remove when actual doctor-patient links are established!
                                                             //if you see this later in production please delete :)
        {
            try
            {
                Random RNG = new Random();
                return RNG.Next(10);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<int> getNumberOfPatientsByAgeGroup() 
        {
            
            try
            {
                List<int> returnList = new List<int>();
                for(int i =0; i<6; i++)
                {
                    returnList.Add(0);
                }   //implemment Gender in Users and in repository make where(x => x.Gender = male/female)
                foreach(Patient patient in _unitOfWork.PatientRepository.GetAll())
                {
                    returnList[getPatientsAgeGroup(patient)]++; //WARNING: NOT IMPLEMENTED
                }
                return returnList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private int getPatientsAgeGroup(Patient patient)
        {
            throw new NotImplementedException();
            /* 
             patientService.getAge(patient) switch {
                age < 16 => return 0
                age < 25 => return 1
                age < ...
            }
             */
        }
    }
}
