using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HospitalDbContext _context;

        public RoomRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll()
        {
            // return _context.Rooms.ToList();
            return _context.Rooms.Include(x => x.Floor.Building)
                                 .Include(x => x.Floor).Include(x => x.WorkingHours)
                                 .Where(x => !x.Deleted)
                                 .ToList();
        }

        public Room GetById(int id)
        {
            return _context.Rooms.Include(x => x.Floor).ThenInclude(x => x.Building).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Room> GetAvailableRooms()
        {
            return _context.Rooms.Include(x => x.Floor)
                                 .ThenInclude(x => x.Building)
                                 .Include(x => x.WorkingHours)
                                 .Include(x => x.Patients)
                                 .Where(x => x.Capacity > x.Patients.Count && !x.Number.Equals("Hallway"))
                                 .ToList();
        }

        public void Create(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(Room room)
        {
            Room roomFromBase = _context.Rooms.Find(room.Id);
            roomFromBase.Purpose = room.Purpose;
            roomFromBase.Number = room.Number;
            _context.Entry(roomFromBase).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
