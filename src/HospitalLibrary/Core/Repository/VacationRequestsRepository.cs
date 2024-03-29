﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
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
                                            .ThenInclude(x => x.Office)
                                            .ThenInclude(x => x.Floor)
                                            .ThenInclude(x => x.Building)
                                            .Include(x => x.Doctor)
                                            .ThenInclude(x => x.WorkHours)
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

        public IEnumerable<VacationRequest> GetAllRejectedByDoctorId(int doctorId)
        {
            return _context.VacationRequests.Include(x => x.Doctor)
                                           .Where(x => !x.Deleted && x.Doctor.Id == doctorId && x.Status == VacationRequestStatus.REJECTED)
                                           .ToList();
        }

        public override VacationRequest Get(int id)
        {
            return _context.VacationRequests.Include(x => x.Doctor)
                                            .FirstOrDefault(x => x.Id == id && !x.Deleted);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public List<VacationRequest> GetAllDoctorId(int doctorId)
        {
            return _context.VacationRequests.Include(x => x.Doctor)
                                           .Where(x => x.Doctor.Id == doctorId && x.Status == VacationRequestStatus.APPROVED)
                                           .ToList();
        }

    }
}
