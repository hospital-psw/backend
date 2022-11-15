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
    }
}
