﻿namespace HospitalLibrary.Core.Repository.Blood
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAcquisitionRepository : BaseRepository<BloodAcquisition>, IBloodAcquisitionRepository
    {
        public BloodAcquisitionRepository(HospitalDbContext context) : base(context)
        {
        }

        public override IEnumerable<BloodAcquisition> GetAll()
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor);
        }


        public override BloodAcquisition Get(int id)
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor)
                                                      .FirstOrDefault(x => x.Id == id);
        }

        public override void Update(BloodAcquisition entity)
        {
            HospitalDbContext.Entry(entity).State = (entity as Entity).Id == 0 ? EntityState.Added : EntityState.Modified;
        }

        public IEnumerable<BloodAcquisition> GetPendingAcquisitions()
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor)
                                                       .Where(x => x.Status == Model.Blood.Enums.BloodRequestStatus.PENDING);
        }

        public IEnumerable<BloodAcquisition> GetAcquisitionsForSpecificDoctor(int id)
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor)
                                                      .Where(x => x.Doctor.Id == id);
        }

        public IEnumerable<BloodAcquisition> GetAllAccepted()
        {
            return GetAll().Where(x => x.Status == BloodRequestStatus.ACCEPTED).ToList();
        }

        public IEnumerable<BloodAcquisition> GetAllDeclined()
        {
            return GetAll().Where(x => x.Status == BloodRequestStatus.DECLINED).ToList();
        }

        public IEnumerable<BloodAcquisition> GetAllPending()
        {
            return GetAll().Where(x => x.Status == BloodRequestStatus.PENDING).ToList();
        }

        public IEnumerable<BloodAcquisition> GetAllReconsidering()
        {
            return GetAll().Where(x => x.Status == BloodRequestStatus.RECONSIDERING).ToList();
        }

    }
}
