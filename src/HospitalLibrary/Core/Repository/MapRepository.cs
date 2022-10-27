namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MapRepository : BaseRepository<RoomMap>, IMapRepository
    {
        private readonly HospitalDbContext _context;
        public MapRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<RoomMap> GetAll()
        {
            return _context.RoomsMap.Include(x => x.Room).Include(x => x.Room.Building).Include(x => x.Room.Floor).Include(x => x.Room.WorkingHours)
                                   .Where(x => !x.Deleted)
                                   .ToList();
        }
    }
}
