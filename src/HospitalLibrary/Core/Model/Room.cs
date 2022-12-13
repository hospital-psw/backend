using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model.ApplicationUser;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Room : Entity
    {
        public string Number { get; private set; }
        public Floor Floor { get; private set; }
        public string Purpose { get; private set; }
        public WorkingHours? WorkingHours { get; private set; }
        public int Capacity { get; private set; }
        public List<ApplicationPatient> Patients { get; private set; } = new List<ApplicationPatient>();
        public List<RenovationRequest> Renovations { get; private set; }


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

        public void UpdatePurpose(string newPurpose)
        {
            this.Purpose = newPurpose;
        }

        public bool NumberStartsWithFloorNumber(string newNumber)
        {
            if (Floor.Number.ToString() == newNumber.Substring(0, 1))
            {
                return true;
            }
            return false;
        }

        public bool UpdateNumber(string newNumber)
        {
            if (NumberStartsWithFloorNumber(newNumber))
            {
                this.Number = newNumber;
                return true;
            }
            return false;
        }

        public void AddPatient(ApplicationPatient patient)
        {
            Patients.Add(patient);
        }
        internal Room(string number, Floor floor, string purpose, WorkingHours workingHours)
        {
            Number = number;
            Floor = floor;
            Purpose = purpose;
            WorkingHours = workingHours;
        }

        public static Room Create(string number, Floor floor, string purpose, WorkingHours workingHours)
        {
            return new Room(number, floor, purpose, workingHours);
        }


        public void Delete()
        {
            Deleted = true;
        }

        public void SetCapacity(int newCapacity)
        {
            Capacity = newCapacity;
        }

        public void SetPatients(List<ApplicationPatient> newPatients)
        {
            Patients = newPatients;
        }
    }
}
