﻿namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BuildingDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }


        public BuildingDetailsDTO(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            Address = building.Address;
        }
    }
}