﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalTreatmentRepository : BaseRepository<MedicalTreatment>, IMedicalTreatmentRepository
    {
        public MedicalTreatmentRepository(HospitalDbContext context) : base(context)
        {
        }

        public override MedicalTreatment Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<MedicalTreatment> GetAll()
        {
            return HospitalDbContext.MedicalTreatments.Include(x => x.Room)
                                                      .ThenInclude(x => x.Floor)
                                                      .ThenInclude(x => x.Building)
                                                      .Include(x => x.Room)
                                                      .ThenInclude(x => x.WorkingHours)
                                                      .Include(x => x.Patient)
                                                      .Include(x => x.Doctor)
                                                      .ThenInclude(x => x.Office)
                                                      .ThenInclude(x => x.Floor)
                                                      .ThenInclude(x => x.Building)
                                                      .Include(x => x.Doctor)
                                                      .ThenInclude(x => x.WorkHours)
                                                      .Include(x => x.MedicamentTherapies)
                                                      .ThenInclude(x => x.Medicament)
                                                      .Include(x => x.BloodUnitTherapies)
                                                      .ThenInclude(x => x.BloodUnit)
                                                      .Where(x => !x.Deleted);
        }

        public IEnumerable<MedicalTreatment> GetActive()
        {
            return GetAll().Where(x => x.Active).ToList();
        }

        public IEnumerable<MedicalTreatment> GetInactive()
        {
            return GetAll().Where(x => !x.Active).ToList();
        }

        public IEnumerable<MedicalTreatment> GetDoctorsActiveTreatments(int doctorId)
        {
            return GetActive().Where(x => x.Doctor.Id == doctorId).ToList();
        }

        public IEnumerable<MedicalTreatment> GetDoctorsInactiveTreatments(int doctorId)
        {
            return GetInactive().Where(x => x.Doctor.Id == doctorId).ToList();
        }
    }
}
