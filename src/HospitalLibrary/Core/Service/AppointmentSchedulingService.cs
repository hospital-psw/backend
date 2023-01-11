namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.Events.Scheduling;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentSchedulingService : BaseService<AppointmentSchedulingRoot>, IAppointmentSchedulingService
    {
        public AppointmentSchedulingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public AppointmentSchedulingRoot StartSession(SessionStarted evt)
        {
            CleanUp(evt.PatientId); //ako je ostala nedovrsena sesija kojoj je poslednji event pre vise od 15 min stavi je na completed
            AppointmentSchedulingRoot root = GetActiveRoot(evt.PatientId);  //dobavi aktivan od tog usera
            if (root is null)    //ako ne nadje pravi novi
            {
                root = AppointmentSchedulingRoot.Create(evt);
                root.LastChange = evt.TimeStamp;
                _unitOfWork.AppointmentSchedulingRootRepository.Add(root);
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
    }
}
