using HospitalLibrary.Core.DTO;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Room : Entity
    {
        public string Number { get; set; }
        public Floor Floor { get; set; }
        public string Purpose { get; set; }
        public WorkingHours? WorkingHours { get; set; }
        public int Capacity { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();

        public Room() { }

        public Room(int id, string number, DateTime dateCreated, DateTime dateUpdated, bool deleted, Floor floor, string purpose, WorkingHours workingHours)
        {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateUpdated;
            this.Deleted = deleted;
            this.Number = number;
            this.Floor = floor;
            this.WorkingHours = workingHours;
            this.Purpose = purpose;
        }
        public Room(int id, string number, Floor floor, string purpose, WorkingHours workingHours) : base(id)
        {
            Number = number;
            Floor = floor;
            Purpose = purpose;
            WorkingHours = workingHours;
        }
    }
}
