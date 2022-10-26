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
        public DateTime date { get; set; }

        public int Duration { get; set; }

        public ExaminationType ExamType { get; set; }

        public bool IsDone { get; set; }

        public Room Room { get; set; }


    }
}
