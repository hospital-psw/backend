namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Appointment : Entity
    {
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public ExaminationType ExamType { get; set; }

        public bool IsDone { get; set; }

        public Room Room { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Appointment appointment && Date == appointment.Date;
        }

        public Appointment() { }

        public Appointment(DateTime date, ExaminationType examType, Room room, Patient patient, Doctor doctor)
        {
            Date = date;
            Duration = 30;
            ExamType = examType;
            IsDone = false;
            Room = room;
            Patient = patient;
            Doctor = doctor;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
