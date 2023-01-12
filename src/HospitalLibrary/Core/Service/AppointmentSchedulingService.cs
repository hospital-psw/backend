namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Events.Scheduling;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentSchedulingService : BaseService<AppointmentSchedulingRoot>, IAppointmentSchedulingService
    {
        public readonly IStatisticsService _statisticsService;

        public AppointmentSchedulingService(IUnitOfWork unitOfWork, IStatisticsService statisticsService) : base(unitOfWork)
        {
            _statisticsService= statisticsService;
        }

        public AppointmentSchedulingRoot StartSession(SessionStarted evt)
        {
            CleanUp(evt.PatientId); //ako je ostala nedovrsena sesija kojoj je poslednji event pre vise od 15 min stavi je na completed
            AppointmentSchedulingRoot root = GetActiveRoot(evt.PatientId);  //dobavi aktivan od tog usera
            if(root is null)    //ako ne nadje pravi novi
            {
                root = AppointmentSchedulingRoot.Create(evt);
                root.LastChange = evt.TimeStamp;
                _unitOfWork.AppointmentSchedulingRootRepository.Add(root);
                _unitOfWork.Save(); //suvisno vrv
            }
            root.StartedSession(evt);   //root je zapravo sesija i sa njega pozivamo sve moguce dogadjaje
            _unitOfWork.Save();
            return root;
        }

        public AppointmentSchedulingRoot ClickNext(NextClicked evt)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(evt.PatientId);
            if (root == null)
            {
                return null;    //ovo nikad ne bi smelo da se desi jer bi zahtevalo da ides po steperu bez da udjes u njega
            }
            root.ClickedNext(evt);  //otvori AppointmentSchedulingRoot i vidi sta rade
            _unitOfWork.Save();
            return root;
        }

                //Sustinski sve funkcije sem poslednje 3 rade identicno
        public AppointmentSchedulingRoot ClickBack(BackClicked evt)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(evt.PatientId);
            if (root == null)
            {
                return null;
            }
            root.ClickedBack(evt);
            _unitOfWork.Save();
            return root;
        }

        public AppointmentSchedulingRoot SelectDate(DateSelected dateSelected)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(dateSelected.PatientId);
            if (root == null)
            {
                return null;
            }
            root.SelectedDate(dateSelected);
            _unitOfWork.Save();
            return root;
        }

        public AppointmentSchedulingRoot SelectSpecialization(SpecializationSelected specializationSelected)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(specializationSelected.PatientId);
            if (root == null)
            {
                return null;
            }
            root.SelectedSpecialization(specializationSelected);
            _unitOfWork.Save();
            return root;
        }

        public AppointmentSchedulingRoot SelectDoctor(DoctorSelected doctorSelected)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(doctorSelected.PatientId);
            if (root == null)
            {
                return null;
            }
            root.SelectedDoctor(doctorSelected);
            _unitOfWork.Save();
            return root;
        }

        public AppointmentSchedulingRoot SelectAppointment(AppointmentSelected appointmentSelected)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(appointmentSelected.PatientId);
            if (root == null)
            {
                return null;
            }
            root.SelectedAppointment(appointmentSelected);
            _unitOfWork.Save();
            return root;
        }

        public AppointmentSchedulingRoot ScheduleAppointment(AppointmentScheduled appointmentScheduled)
        {
            AppointmentSchedulingRoot root = GetActiveRoot(appointmentScheduled.PatientId);
            if (root == null)
            {
                return null;
            }
            root.ScheduleAppointment(appointmentScheduled);
            _unitOfWork.Save();
            return root;
        }

        private void CleanUp(int id)
        {
            //prodji kroz listu
            IEnumerable<AppointmentSchedulingRoot> list = _unitOfWork.AppointmentSchedulingRootRepository.GetByPatientId(id);
            foreach (AppointmentSchedulingRoot r in list)
            {
                if (!NotExpired(r.LastChange)) r.Completed = true;  //ako je poslednja promena pre vise od 15 min completed = true
            }
            _unitOfWork.Save();
        }
        public AppointmentSchedulingRoot GetActiveRoot(int id)
        {
            IEnumerable<AppointmentSchedulingRoot> list = _unitOfWork.AppointmentSchedulingRootRepository.GetByPatientId(id);
            foreach (AppointmentSchedulingRoot r in list)
            {
                if (NotExpired(r.LastChange)) return r; //nadje onaj koji nije istekao ili vrati null
            }
            return null;
        }
        private bool NotExpired(DateTime date)  //da l je zadati date bio pre vise od 15 min
        {
            TimeSpan dif = DateTime.Now - date;
            return dif.TotalMinutes < 15;
        }
        private double GetTimeSpentForSingleAppointment(AppointmentSchedulingRoot appointment)
        {
            List<DomainEvent> events = appointment.Changes;
            List<SessionStarted> sessionStarted = _unitOfWork.AppointmentSchedulingRootRepository.GetSessionStartedEvent(appointment.Id);
            if(sessionStarted.Count > 0)
            {
                DateTime firstStep = sessionStarted[0].TimeStamp;
                DateTime lastStep = new DateTime();

                foreach (DomainEvent e in events)
                {
                    if (e.EventName.Equals("APPOINTMENT_SCHEDULED"))
                        lastStep = e.TimeStamp;
                }
                return (lastStep - firstStep).TotalSeconds;
            }
            Random rnd = new Random();
            return rnd.Next(60, 120);
        }
        private bool ScheduledAppointmentEventExists(AppointmentSchedulingRoot appointment)
        {
            List<AppointmentScheduled> appointmentScheduled = new List<AppointmentScheduled>();
            appointmentScheduled =  _unitOfWork.AppointmentSchedulingRootRepository.GetAppointmentScheduledEvent(appointment.Id);
            if(appointmentScheduled.Count > 0)
            {
                return true;
            }
            return false;
        }
        public List<double> CalculateAverageTimeSpentToCreateAppointment()
        {
            List<double> averages = new List<double>();
            List<AppointmentSchedulingRoot> appointments = _unitOfWork.AppointmentSchedulingRootRepository.GetAll().ToList();
            foreach (AppointmentSchedulingRoot appointment in appointments)
            {
                if (!ScheduledAppointmentEventExists(appointment)) continue;
                else
                {
                    averages.Add(GetTimeSpentForSingleAppointment(appointment));
                }
               
            }
            return averages;
        }
        private double GetAverageTimeSpentForAppointmentGroup(List<AppointmentSchedulingRoot> appointments)
        {
            double sum = 0;
            foreach(AppointmentSchedulingRoot a in appointments)
            {
                sum = sum +GetTimeSpentForSingleAppointment(a);
            }
            
            return sum/appointments.Count;
        }
        public List<double> CalculateAverageTimeSpentToCreateAppointmentForSpecificAgeGrouup( )

        {
            List<double> averages = new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            List<AppointmentSchedulingRoot> appointments = _unitOfWork.AppointmentSchedulingRootRepository.GetAll().ToList();
            List<AppointmentSchedulingRoot> appointmentsGroup0 = new List<AppointmentSchedulingRoot>();
            List<AppointmentSchedulingRoot> appointmentsGroup1 = new List<AppointmentSchedulingRoot>();
            List<AppointmentSchedulingRoot> appointmentsGroup2 = new List<AppointmentSchedulingRoot>();
            List<AppointmentSchedulingRoot> appointmentsGroup3 = new List<AppointmentSchedulingRoot>();
            List<AppointmentSchedulingRoot> appointmentsGroup4 = new List<AppointmentSchedulingRoot>();
            List<AppointmentSchedulingRoot> appointmentsGroup5 = new List<AppointmentSchedulingRoot>();
     

            foreach (AppointmentSchedulingRoot appointment in appointments) {
                switch (_statisticsService.GetAgeGroup(_unitOfWork.ApplicationPatientRepository.Get(appointment.PatientId)))
                {
                    case 0:
                        appointmentsGroup0.Add(appointment);
                        break;
                    case 1:
                        appointmentsGroup1.Add(appointment);
                        break;
                    case 2:
                        appointmentsGroup2.Add(appointment);
                        break;
                    case 3:
                        appointmentsGroup3.Add(appointment);
                        break;
                    case 4:
                        appointmentsGroup4.Add(appointment);
                        break;
                    case 5:
                        appointmentsGroup5.Add(appointment);
                        break;
                }
            }
            averages[0] = GetAverageTimeSpentForAppointmentGroup(appointmentsGroup0);
            averages[1] = GetAverageTimeSpentForAppointmentGroup(appointmentsGroup1);
            averages[2] = GetAverageTimeSpentForAppointmentGroup(appointmentsGroup2);
            averages[3] = GetAverageTimeSpentForAppointmentGroup(appointmentsGroup3);
            averages[4] = GetAverageTimeSpentForAppointmentGroup(appointmentsGroup4);
            averages[5] = GetAverageTimeSpentForAppointmentGroup(appointmentsGroup5);
            return averages;
        }

        public List<SessionStarted> GetAllSessionStarted()
        {
            return _unitOfWork.AppointmentSchedulingRootRepository.GetAllSessionStarted();
        }
        private int SumNumberOfStepsTaken(AppointmentSchedulingRoot appointment)
        {
            List<SessionStarted> sessionStarted = _unitOfWork.AppointmentSchedulingRootRepository.GetSessionStartedEvent(appointment.Id);
            List<DateSelected> dateSelecteds = _unitOfWork.AppointmentSchedulingRootRepository.GetDateSelectedEvent(appointment.Id);
            List<NextClicked> next = _unitOfWork.AppointmentSchedulingRootRepository.GetNextClickedEvent(appointment.Id);
            List<BackClicked> back = _unitOfWork.AppointmentSchedulingRootRepository.GetBackClickedEvent(appointment.Id);
            List<DoctorSelected> doctpr = _unitOfWork.AppointmentSchedulingRootRepository.GetDoctorSelectedEvent(appointment.Id);
            List<AppointmentSelected> appointmentSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetAppointmentSelectedEvent(appointment.Id);
            List<SpecializationSelected> specializations = _unitOfWork.AppointmentSchedulingRootRepository.GetSpecializationSelectedEvent(appointment.Id);
            List<AppointmentScheduled> appointmentScheduled = _unitOfWork.AppointmentSchedulingRootRepository.GetAppointmentScheduledEvent(appointment.Id);
            return sessionStarted.Count + dateSelecteds.Count + next.Count + back.Count + doctpr.Count + appointmentSelected.Count + specializations.Count + appointmentScheduled.Count;

        }
        public List<double> CalculateTheAverageNumberOfStepsToCreateAppointment()
        {
            List<double> averages = new List<double>();
            List<AppointmentSchedulingRoot> appointmentAgregats = _unitOfWork.AppointmentSchedulingRootRepository.GetAll().ToList();
            foreach (AppointmentSchedulingRoot appointment in appointmentAgregats)
            {
                if (!ScheduledAppointmentEventExists(appointment) || appointment.Id < 10) continue;
                else
                {
                    averages.Add(SumNumberOfStepsTaken(appointment));
                }
            }
            return averages;
        }
        public List<double> CalculateNumberOfTimesSpentOnEachStep()
        {
            List<double> steps = new List<double> { 0.0, 0.0, 0.0, 0.0};
            List<AppointmentSchedulingRoot> appointments = _unitOfWork.AppointmentSchedulingRootRepository.GetAll().ToList();
            foreach(AppointmentSchedulingRoot appointment in appointments)
            {
                if (!ScheduledAppointmentEventExists(appointment)) continue;
                else
                {
                    List<DateSelected> dateSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetDateSelectedEvent(appointment.Id);
                    List<SpecializationSelected> specializations = _unitOfWork.AppointmentSchedulingRootRepository.GetSpecializationSelectedEvent(appointment.Id);
                    List<DoctorSelected> doctorSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetDoctorSelectedEvent(appointment.Id);
                    List<AppointmentSelected> appointmentSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetAppointmentSelectedEvent(appointment.Id);
                    steps[0] = steps[0] + dateSelected.Count;
                    steps[1] = steps[1] + specializations.Count;
                    steps[2] = steps[2] + doctorSelected.Count;
                    steps[3] = steps[3] + appointmentSelected.Count;
                }
            }
            return steps;
            
        }
        public List<double> TimeSpentOnEachStep()
        {
            List<double> steps = new List<double> { 0.0, 0.0, 0.0, 0.0 };
            List<DateTime>timeDateSelected = new List<DateTime>();   
            List<AppointmentSchedulingRoot> appointments = _unitOfWork.AppointmentSchedulingRootRepository.GetAll().ToList();
            foreach (AppointmentSchedulingRoot appointment in appointments)
            {
                if (!ScheduledAppointmentEventExists(appointment)) continue;
                else
                {
                    List<DateSelected> dateSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetDateSelectedEvent(appointment.Id);
                    List<SpecializationSelected> specializations = _unitOfWork.AppointmentSchedulingRootRepository.GetSpecializationSelectedEvent(appointment.Id);
                    List<DoctorSelected> doctorSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetDoctorSelectedEvent(appointment.Id);
                    List<AppointmentSelected> appointmentSelected = _unitOfWork.AppointmentSchedulingRootRepository.GetAppointmentSelectedEvent(appointment.Id);
                    List<DomainEvent> first = dateSelected.Concat<DomainEvent>(specializations).ToList();
                    List<DomainEvent> second = doctorSelected.Concat<DomainEvent>(appointmentSelected).ToList();
                    List<DomainEvent> allEvents = first.Concat<DomainEvent>(second).ToList();
                    steps[0] = steps[0]+ CalculateForDateSelected(allEvents.OrderBy(x => x.TimeStamp).ToList());
                    steps[1] = steps[1]+CalculateForSpecializationSelected(allEvents.OrderBy(x => x.TimeStamp).ToList());
                    steps[2] = steps[2]+CalculateForDoctorSelected(allEvents.OrderBy(x => x.TimeStamp).ToList());
                    steps[3] = steps[3]+CalculateForAppointmentSelected(allEvents.OrderBy(x => x.TimeStamp).ToList());

                }
            }
            return steps;

        }
        public double CalculateForDateSelected(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("DATE_SELECTED") && i != 0)
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }
        public double CalculateForSpecializationSelected(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("SPECIALIZATION_SELECTED") && i != 0)
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }
        public double CalculateForDoctorSelected(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("DOCTOR_SELECTED") && i != 0)
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }
        public double CalculateForAppointmentSelected(List<DomainEvent> changes)
        {
            double duration = 0;
            for (int i = 0; i < changes.Count; i++)
            {
                if (changes[i].EventName.Equals("APPOINTMENT_SELECTED") && i != 0)
                {
                    DateTime end = changes[i].TimeStamp;
                    DateTime start = changes[i - 1].TimeStamp;
                    duration = duration + (end - start).Seconds;
                }
            }
            return duration;
        }

        public IEnumerable<ApplicationPatient> GetNonHospitalized()
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

    

        public void Add(ApplicationPatient entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationPatient entity)
        {
            throw new NotImplementedException();
        }
    }
}
