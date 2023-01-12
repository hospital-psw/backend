namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.RenovationRequest;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
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
    using System.Xml.Linq;

    public class StatisticsService : IStatisticsService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IRenovationService _renovationService;

        public StatisticsService(IUnitOfWork unitOfWork, IRenovationService renovationService)
        {
            _unitOfWork = unitOfWork;
            _renovationService = renovationService;
        }

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
            List<int> retList = new();
            var allMonths = from month in Enumerable.Range(1, 12)
                            let key = new { Month = month }
                            join appointment in _unitOfWork.AppointmentRepository.GetAll().Where(a => a.Doctor.Id == doctorId && a.Date.Year == year) on key
                            equals new { appointment.Date.Month } into g
                            select new { key, total = g.Count() };
            foreach (var element in allMonths)
            {
                retList.Add(element.total);
            }
            return retList;
        }

        public List<int> GetNumberOfDoctorAppointmentsPerMonth(int doctorId, int month, int year)
        {
            //List<int> retList = new();
            //var allDays = from day in CreateMonthList(month)
            //                let key = new { Day = day }
            //                join appointment in _unitOfWork.AppointmentRepository.GetAll().Where(a => a.Doctor.Id == doctorId && a.Date.Month == month && a.Date.Year == year) on key
            //                equals new { appointment.Date.Day } into g
            //                select new { key, total = g.Count() };
            //Console.WriteLine(allDays);

            //foreach (var element in allDays)
            //{
            //    Console.WriteLine(element);
            //    retList.Add(element.total);
            //}
            //return retList;
            try
            {
                List<int> retList = CreateMonthList(month);
                List<Appointment> appointments = _unitOfWork.AppointmentRepository.GetMonthlyAppointmentsForDoctor(doctorId, year, month).ToList();
                foreach (Appointment appointment in appointments)
                {
                    retList[appointment.Date.Day - 1] = retList[appointment.Date.Day - 1] + 1;
                }
                return retList;
            }
            catch (Exception)
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

        public List<double> GetNumberOfViewsForEachStep()
        {
            List<double> retList = new();
            List<string> evtNames = new()
            {
                "RENOVATION_TYPE_EVENT",
                "ROOMS_EVENT",
                "DATE_PICK_EVENT",
                "DURATION_EVENT",
                "START_TIME_EVENT",
                "PREVIOUS_EVENT_1",
                "PREVIOUS_EVENT_2",
                "PREVIOUS_EVENT_3",
                "PREVIOUS_EVENT_4",
                "PREVIOUS_EVENT_5"
            };
            var allSteps = from eventName in evtNames
                           let key = new { EventName = eventName }
                           join renovationEvent in _unitOfWork.RenovationEventRepository.GetAll() on key
                           equals new { renovationEvent.EventName } into g
                           select new { key, total = g.Count() };

            retList.Add(_unitOfWork.RenovationRepository.GetAllAggregates().Count);
            foreach (var element in allSteps)
            {
                if (element.key.EventName == "PREVIOUS_EVENT_1") retList[0] += element.total;
                else if (element.key.EventName == "PREVIOUS_EVENT_2") retList[1] += element.total;
                else if (element.key.EventName == "PREVIOUS_EVENT_3") retList[2] += element.total;
                else if (element.key.EventName == "PREVIOUS_EVENT_4") retList[3] += element.total;
                else if (element.key.EventName == "PREVIOUS_EVENT_5") retList[4] += element.total;
                else retList.Add(element.total);
            }
            return retList;
        }

        public List<double> GetNumberOfStepsAccordingToRenovationType()
        {
            List<double> retList = ListFactory.CreateList<double>(0, 0);
            int merge = 0;
            int split = 0;
            foreach (RenovationRequest request in _renovationService.GetAllSuccessfulAggregates())
            {

                if (request.RenovationType == RenovationType.MERGE)
                {
                    merge++;
                    retList[0] += request.Changes.Count;
                }
                else
                {
                    split++;
                    retList[1] += request.Changes.Count;
                }
            }

            if (merge > 0) retList[0] = retList[0] / merge;
            if (split > 0) retList[1] = retList[1] / split;
            return retList;
        }

        public List<double> GetAverageSchedulingDurationByGroups()
        {
            List<double> averages = new List<double>();
            List<RenovationRequest> requests = _unitOfWork.RenovationRepository.GetAllEverMade().ToList();
            foreach (RenovationRequest request in requests)
            {
                if (!DoesScheduleEventExists(request)) continue;
                averages.Add(CalculateAverageTimeForSingleRenovationScheduling(request));
            }
            return Structure(averages);
        }

        public List<double> GetAverageSchedulingDuration()
        {
            List<double> averages = new List<double>();
            List<RenovationRequest> requests = _unitOfWork.RenovationRepository.GetAllEverMade().ToList();
            foreach (RenovationRequest request in requests)
            {
                if (!DoesScheduleEventExists(request)) continue;
                averages.Add(CalculateAverageTimeForSingleRenovationScheduling(request));
            }
            return averages;
        }

        public List<double> GetAverageSchedulingDurationBasedOnRenovationType()
        {
            List<double> averagesMerge = new List<double>();
            List<double> averagesSplit = new List<double>();
            List<double> result = new List<double>();

            List<RenovationRequest> requests = _unitOfWork.RenovationRepository.GetAllEverMade().ToList();
            foreach (RenovationRequest request in requests)
            {
                if (!DoesScheduleEventExists(request)) continue;
                if (request.RenovationType == RenovationType.MERGE)
                    averagesMerge.Add(CalculateAverageTimeForSingleRenovationScheduling(request));
                else
                    averagesSplit.Add(CalculateAverageTimeForSingleRenovationScheduling(request));
            }
            result.Add(CalculateAveragesForType(averagesMerge));
            result.Add(CalculateAveragesForType(averagesSplit));
            return result;
        }

        private double CalculateAveragesForType(List<double> averages)
        {
            double average = 0;
            foreach (double num in averages)
            {
                average += num;
            }
            return averages.Count == 0 ? 0 : average / averages.Count;
        }

        private List<double> Structure(List<double> averages)
        {
            List<double> structure = new List<double>() { 0, 0, 0, 0, 0 };
            foreach (double num in averages)
            {
                if (num <= 30)
                    structure[0]++;
                else if (num > 30 && num <= 60)
                    structure[1]++;
                else if (num > 60 && num <= 90)
                    structure[2]++;
                else if (num > 90 && num <= 120)
                    structure[3]++;
                else
                    structure[4]++;
            }
            return structure;
        }

        private double CalculateAverageTimeForSingleRenovationScheduling(RenovationRequest request)
        {
            List<DomainEvent> events = request.Changes;
            DateTime firstStep = new DateTime();
            DateTime lastStep = new DateTime();

            foreach (RenovationEvent e in events)
            {
                if (e.EventName.Equals("RENOVATION_TYPE_EVENT"))
                    firstStep = e.TimeStamp;
                else if (e.EventName.Equals("SCHEDULE_EVENT"))
                    lastStep = e.TimeStamp;
            }
            return (lastStep - firstStep).Seconds;
        }

        private bool DoesScheduleEventExists(RenovationRequest request)
        {
            foreach (RenovationEvent evt in request.Changes)
            {
                if (evt.EventName.Equals("SCHEDULE_EVENT"))
                    return true;
            }
            return false;
        }

        public List<double> GetAverageNumberOfRenovationSteps()
        {
            List<double> retList = new List<double>();
            var list = _renovationService.GetAllSuccessfulAggregates().GroupBy(r => new { ID = r.DateCreated.Year })
                .Select(g => new { Average = g.Average(p => p.Changes.Count), ID = g.Key.ID });
            foreach (var r in list)
            {
                retList.Add(r.Average);
            }
            return retList;
        }

        public List<RenovationStatisticDto> GetTimeSpentPerStep()
        {
            List<RenovationStatisticDto> dtos = new List<RenovationStatisticDto>();
            List<RenovationRequest> requests = _unitOfWork.RenovationRepository.GetAllEverMade().ToList();

            foreach (RenovationRequest request in requests)
            {
                if (!DoesScheduleEventExists(request)) continue;
                RenovationStatisticDto dto = new RenovationStatisticDto();
                dto.Date = request.StartTime;
                dto.Step1 = CalculateForRenovationTypeStep(request.Changes.OrderBy(x => x.TimeStamp).ToList());
                dto.Step2 = CalculateForRoomsStep(request.Changes.OrderBy(x => x.TimeStamp).ToList());
                dto.Step3 = CalculateForDatePickStep(request.Changes.OrderBy(x => x.TimeStamp).ToList());
                dto.Step4 = CalculateForDurationStep(request.Changes.OrderBy(x => x.TimeStamp).ToList());
                dto.Step5 = CalculateForStartTimeStep(request.Changes.OrderBy(x => x.TimeStamp).ToList());
                dto.Step6 = CalculateForScheduleStep(request.Changes.OrderBy(x => x.TimeStamp).ToList());
                dtos.Add(dto);
            }
            return dtos;
        }

        public double CalculateForRenovationTypeStep(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("RENOVATION_TYPE_EVENT") && i != 0)
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public double CalculateForRoomsStep(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("ROOMS_EVENT"))
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public double CalculateForDatePickStep(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("DATE_PICK_EVENT"))
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public double CalculateForDurationStep(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("DURATION_EVENT"))
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public double CalculateForStartTimeStep(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("START_TIME_EVENT"))
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public double CalculateForScheduleStep(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("SCHEDULE_EVENT"))
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public List<int> GetNumberOfDoctorAppointmentsInOptionalTimeRange(int doctorId, DateTime start, DateTime end)
        {
            try
            {
                List<int> retList = new List<int>();
                if ((end - start).TotalDays < 32)
                {
                    retList = CalculateForDays(doctorId, start, end);
                }
                else
                {
                    retList = CalculateForMonths(doctorId, start, end);
                }
                return retList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<int> CalculateForDays(int doctorId, DateTime start, DateTime end)
        {
            List<Appointment> appointments = _unitOfWork.AppointmentRepository.GetAppointmentsForDoctorInDateRange(doctorId, start, end).ToList();
            List<int> retList = new List<int>();
            for (int i = 0; i <= (int)(end - start).TotalDays; i++)
            {
                retList.Add(0);
            }
            foreach (Appointment a in appointments)
            {
                int index = (int)(a.Date - start).TotalDays;
                retList[index] = retList[index] + 1;
            }
            return retList;
        }

        private List<int> CalculateForMonths(int doctorId, DateTime start, DateTime end)
        {
            List<Appointment> appointments = _unitOfWork.AppointmentRepository.GetAppointmentsForDoctorInDateRange(doctorId, start, end).ToList();
            List<int> retList = new List<int>();
            int months = Math.Abs((end.Month - start.Month) + 12 * (end.Year - start.Year));
            for (int i = 0; i <= months; i++)
            {
                retList.Add(0);
            }
            foreach (Appointment a in appointments)
            {
                int index = a.Date.Month - start.Month;
                retList[index] = retList[index] + 1;
            }
            return retList;
        }
    }
}
