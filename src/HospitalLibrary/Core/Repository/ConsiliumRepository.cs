﻿namespace HospitalLibrary.Core.Repository
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
    using System.Data;

    public class ConsiliumRepository : BaseRepository<Consilium>, IConsiliumRepository
    {
        public ConsiliumRepository(HospitalDbContext context) : base(context) { }

        public override Consilium Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Consilium> GetAll()
        {
            return HospitalDbContext.Consiliums.Include(x => x.DoctorsSchedule)
                                               .ThenInclude(x => x.Doctor)
                                               .Include(x => x.Room)
                                               .ThenInclude(x => x.Floor)
                                               .ThenInclude(x => x.Building)
                                               .Include(x => x.Room)
                                               .ThenInclude(x => x.WorkingHours)
                                               .Where(x => !x.Deleted)
                                               .ToList();
        }

        public IEnumerable<Consilium> GetConsiliumsByDoctorId(int doctorId)
        {
            return GetAll().Where(x => x.DoctorsSchedule.Exists(x => x.Doctor.Id == doctorId))
                           .ToList();
        }

        public List<Consilium> GetScheduledConsiliumsForRoom(int roomId)
        {
            return HospitalDbContext.Consiliums.Include(x => x.DoctorsSchedule)
                                               .Where(x => !x.Deleted && x.Room.Id == roomId)
                                               .OrderBy(x => x.DateTime)
                                               .Distinct()
                                               .ToList();
        }
    }
}
