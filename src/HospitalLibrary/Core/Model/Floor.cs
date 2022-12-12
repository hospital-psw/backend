namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Floor : Entity
    {
        public FloorNumber Number { get; set; }
        public string Purpose { get; set; }
        public Building Building { get; set; }

        public Floor() { }

        public Floor(int id, DateTime dateCreated, DateTime dateUpdated, bool deleted, FloorNumber number, string purpose, Building building)
        {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateUpdated;
            this.Deleted = deleted;
            this.Number = number;
            this.Purpose = purpose;
            this.Building = building;
        }
    }
}
