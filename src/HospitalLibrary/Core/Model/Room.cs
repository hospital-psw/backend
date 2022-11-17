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

    }
}
