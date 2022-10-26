namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public class BuildingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public BuildingDTO() { }
        public BuildingDTO(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            Address = building.Address;
        }
    }
}
