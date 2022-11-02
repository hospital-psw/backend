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

        public IEnumerable<Building> GetBuildings()
        {
            return _context.Buildings.ToList();
        }

        public IEnumerable<RoomMap> GetBuildingRooms(int buildingId)
        {
            return _context.RoomsMap.Include(x => x.Room).Include(x => x.Room.Floor.Building).Include(x => x.Room.Floor).Include(x => x.Room.WorkingHours)
                                   .Where(x => !x.Deleted).Where(x => x.Room.Floor.Building.Id == buildingId)
                                   .ToList();
        }

        public IEnumerable<RoomMap> GetFloorRooms(int buildingId, int floor)
        {
            return _context.RoomsMap.Include(x => x.Room).Include(x => x.Room.Floor.Building).Include(x => x.Room.Floor).Include(x => x.Room.WorkingHours)
                                   .Where(x => !x.Deleted).Where(x => x.Room.Floor.Building.Id == buildingId && x.Room.Floor.Number == floor)
                                   .ToList();
        }
    }
}
