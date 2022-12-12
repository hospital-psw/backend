namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryEquipmentRepository : IEquipmentRepository
    {
        public void Add(Equipment entity)
        {
            throw new NotImplementedException();
        }

        public Equipment Create(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public Equipment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipment> GetAll()
        {
            var equipments = new List<Equipment>();

            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0));
            Building building = new Building(4, new DateTime(), new DateTime(), false, "Hospital2", "Janka Cmelika 1");
            Floor floor = new Floor(2, new DateTime(), new DateTime(), false, FloorNumber.Create(0), "ortopedija", building);
            Room room1 = new Room(14, "001", new DateTime(), new DateTime(), false, floor, "ordinacija", workingHours);
            Room room2 = new Room(16, "003", new DateTime(), new DateTime(), false, floor, "ordinacija", workingHours);

            Equipment equipment1 = new Equipment(HospitalLibrary.Core.Model.Enums.EquipmentType.BED, 10, room2);
            Equipment equipment2 = new Equipment(HospitalLibrary.Core.Model.Enums.EquipmentType.SCISSORS, 7, room2);

            equipments.Add(equipment1);
            equipments.Add(equipment2);

            return equipments;
        }

        public Equipment GetEquipment(EquipmentType type, Room room)
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetEquipmentForRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipment> GetEquipments()
        {
            var equipments = new List<Equipment>();

            WorkingHours workingHours = new WorkingHours(5, new DateTime(), new DateTime(), false, new DateTime(2022, 11, 10, 4, 0, 0), new DateTime(2022, 11, 10, 7, 0, 0));
            Building building = new Building(4, new DateTime(), new DateTime(), false, "Hospital2", "Janka Cmelika 1");
            Floor floor = new Floor(2, new DateTime(), new DateTime(), false, FloorNumber.Create(0), "ortopedija", building);
            Room room1 = new Room(14, "001", new DateTime(), new DateTime(), false, floor, "ordinacija", workingHours);
            Room room2 = new Room(16, "003", new DateTime(), new DateTime(), false, floor, "ordinacija", workingHours);

            Equipment equipment1 = new Equipment(HospitalLibrary.Core.Model.Enums.EquipmentType.BED, 10, room2);
            Equipment equipment2 = new Equipment(HospitalLibrary.Core.Model.Enums.EquipmentType.SCISSORS, 7, room2);

            equipments.Add(equipment1);
            equipments.Add(equipment2);

            return equipments;
        }

        public List<Equipment> GetSameEquipmentInRoom(Room room, EquipmentType type)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Equipment entity)
        {
            throw new NotImplementedException();
        }
    }
}
