using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly HospitalDbContext _context;
        public RoomRepository(HospitalDbContext context) : base(context) 
        {
            _context = context;
        }

        public override IEnumerable<Room> GetAll()
        {
            return _context.Rooms.Include(x => x.Floor).Include(x => x.Building).Include(x=>x.WorkingHours)
                                   .Where(x => !x.Deleted)
                                   .ToList();
        }
    }
}