namespace HospitalLibrary.Core.Model
{
    using System;

    public class WorkingHours : Entity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkingHours() {}

    }
}
