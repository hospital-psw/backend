﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequestsRepository : BaseRepository<VacationRequest>, IVacationRequestsRepository
    {
        private readonly HospitalDbContext _context;
        public VacationRequestsRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<VacationRequest> GetAllPending()
        {
            return _context.VacationRequests.Include(x => x.Doctor).
                                            Where(x => x.Status == 0).Where(x => !x.Deleted).
                                            ToList();
        }

        public IEnumerable<VacationRequest> GetAllRequestsByDoctorsId(int doctorId)
        {
            return _context.VacationRequests.Include(x => x.Doctor)
                                            .Where(x => !x.Deleted && x.Doctor.Id == doctorId)
                                            .ToList();
        }
        public IEnumerable<VacationRequest> GetAllWaitingByDoctorId(int doctorId)
        {
            return _context.VacationRequests.Include(x => x.Doctor)
                                           .Where(x => !x.Deleted && x.Doctor.Id == doctorId && x.Status == VacationRequestStatus.WAITING)
                                           .ToList();
        }

        public IEnumerable<VacationRequest> GetAllApprovedByDoctorId(int doctorId)
        {
            return _context.VacationRequests.Include(x => x.Doctor)
                                           .Where(x => !x.Deleted && x.Doctor.Id == doctorId && x.Status == VacationRequestStatus.APPROVED)
                                           .ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
