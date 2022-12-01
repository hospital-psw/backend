namespace HospitalLibrary.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScheduleConsiliumException : Exception
    {
        public string DoctorName { get; set; }
        public ScheduleConsiliumException() { }
        public ScheduleConsiliumException(string message) : base(message) { }
        public ScheduleConsiliumException(string message, Exception exception) : base(message, exception) { }
        public ScheduleConsiliumException(string message, string doctorName) : base(message)
        {
            DoctorName = doctorName;
        }
    }
}
