namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
    {
        private readonly HospitalDbContext _context;
        public EquipmentRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Equipment> GetEquipments()
        {
            return _context.Equipments.Include(x => x.Room).ThenInclude(x => x.Floor).ThenInclude(x => x.Building).ToList();
        }

        public Equipment GetEquipment(EquipmentType type, Room room)
        {
            return GetEquipments().FirstOrDefault(x => !x.Deleted && (x.EquipmentType == type) && (x.Room.Id == room.Id));
        }

        public List<Equipment> GetEquipmentForRoom(Room room) {
            return GetEquipments().Where(x => !x.Deleted && x.Room.Id == room.Id).ToList();
        }

        public Equipment Create(Equipment equipment)
        {
            _context.Equipments.Add(equipment);
            HospitalDbContext.SaveChanges();
            return equipment;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public List<Equipment> GetSameEquipmentInRoom(Room room, EquipmentType type)
        {
            return GetEquipments().Where(x => !x.Deleted && x.Room.Id == room.Id && x.EquipmentType == type).ToList();
        }
    }
}
