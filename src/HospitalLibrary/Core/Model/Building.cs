using HospitalLibrary.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Building : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public Building() { }

        public Building(int id, DateTime dateCreated, DateTime dateUpdated, bool deleted, string name, string address)
        {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateUpdated;
            this.Deleted = deleted;
            this.Name = name;
            this.Address = address;
        }

    }
}
