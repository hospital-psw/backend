namespace HospitalLibrary.Core.Model
{
    using System;

    public class WorkingHours : Entity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkingHours() { }

        public WorkingHours(int id, DateTime dateCreated, DateTime dateUpdated, bool deleted, DateTime start, DateTime end) 
        { 
            this.Id = id;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateUpdated;
            this.Deleted = deleted;
            this.Start = start;
            this.End = end;
        }

        public WorkingHours(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
