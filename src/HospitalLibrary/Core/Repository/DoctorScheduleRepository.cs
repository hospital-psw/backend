﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorScheduleRepository : BaseRepository<DoctorSchedule>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(HospitalDbContext context) : base(context)
        {
        }

        public override DoctorSchedule Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<DoctorSchedule> GetAll()
        {
            return HospitalDbContext.DoctorSchedules.Include(x => x.Doctor)
                                                    .Include(x => x.Consiliums)
                                                    .Include(x => x.Appointments)
                                                    .Include(x => x.VacationRequests)
                                                    .Where(x => !x.Deleted)
                                                    .ToList();
        }

        public IEnumerable<DoctorSchedule> GetDoctorSchedulesByDoctorList(List<ApplicationDoctor> doctorList)
        {
            return GetAll().Where(x => doctorList.Contains(x.Doctor))
                            .ToList();
        }

        public DoctorSchedule GetDoctorScheduleByDoctorId(int doctorId)
        {
            return GetAll().FirstOrDefault(x => x.Doctor.Id == doctorId);
        }
    }
}
