namespace HospitalLibrary.Core.Model.Events.Scheduling.Root
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentSchedulingRoot : EventSourcedAggregate
    {
        public int PatientId { get; set; }  //sustinski user
        public int? DoctorId { get; set; }
        public DateTime? Date { get; set; } //datum koji bira na date pickeru
        public Specialization? Specialization { get; set; }
        public DateTime? Time { get; set; } //vreme termina koji je izabrao korisnik
        public bool Completed { get; set; }
        public DateTime LastChange { get; set; }    //vreme poslednjeg eventa (da znam ako je istekla sesija)

        private AppointmentSchedulingRoot() { }

        private AppointmentSchedulingRoot(SessionStarted evt)   //sakriven konstruktor NAMERNO da moze da se poziva samo preko eventa
        {
            Version = 0;
            PatientId = evt.PatientId;
            Completed = false;
        }

        public static AppointmentSchedulingRoot Create(SessionStarted evt)
        {
            return new AppointmentSchedulingRoot(evt);
        }

        public void Causes(DomainEvent @event)
        {
            Changes.Add(@event);    //dodaje u listu eventova ovog agregata (odnosno sesije)
            Apply(@event);  //svi eventovi nasledjuju domain event pa mogu svi razliciti da se proslede ovoj funkciji
        }                   //i da urade drugaciju stvar

        public override void Apply(DomainEvent changes)
        {
            When((dynamic)changes); //when - drugacija u zavisnosti od pozvanog eventa
            Version++;
        }

        //OVO JE KAKO AGREGAT ILI SESIJA REAGUEJ NA KOJI DOGADJAJ

        public void When(SessionStarted @event)
        {
            //nista se ne desi sa agregatom
        }
        public void When(NextClicked @event)
        {
            //nista se ne desi sa agregatom
        }
        public void When(BackClicked @event)
        {
            //nista se ne desi sa agregatom
        }
        public void When(DateSelected @event)
        {
            Date = @event.Date;
        }
        public void When(SpecializationSelected @event)
        {
            Specialization = @event.Specialization;
        }
        public void When(DoctorSelected @event)
        {
            DoctorId = @event.DoctorId;
        }
        public void When(AppointmentSelected @event)
        {
            Time = @event.DateTime;
        }
        public void When(AppointmentScheduled @event)
        {
            Completed = true;
        }

        // OVO DOLE SU ZAPRAVO FUNCKIJE KOJE IMA NASA SESIJA
        public void StartedSession(SessionStarted evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void ClickedNext(NextClicked evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void ClickedBack(BackClicked evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void SelectedDate(DateSelected evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void SelectedSpecialization(SpecializationSelected evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void SelectedDoctor(DoctorSelected evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void SelectedAppointment(AppointmentSelected evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
        public void ScheduleAppointment(AppointmentScheduled evt)
        {
            LastChange = evt.TimeStamp;
            Causes(evt);
        }
    }
}
