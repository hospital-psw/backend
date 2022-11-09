namespace HospitalLibrary.Core.Model.Therapy
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Therapy : Entity
    {

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string About { get; set; }

        public TherapyType Type { get; set; }

        public Therapy()
        {

        }

        public Therapy(DateTime start, DateTime end, string about, TherapyType type)
        {
            Start = start;
            End = end;
            About = about;
            Type = type;
        }

    }
}
