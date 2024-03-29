﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.AppUsers;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Blood;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Repository.Examinations;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly HospitalDbContext _context;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(HospitalDbContext context)
        {
            _context = context;

            UserRepository = new UserRepository(_context);
            RoomRepository = new RoomRepository(_context);
            FloorRepository = new FloorRepository(_context);
            BuildingRepository = new BuildingRepository(_context);
            WorkingHoursRepository = new WorkingHoursRepository(_context);
            MapRepository = new MapRepository(_context);
            AppointmentRepository = new AppointmentRepository(_context);
            FeedbackRepository = new FeedbackRepository(_context);
            AppointmentRepository = new AppointmentRepository(_context);
            AllergiesRepository = new AllergiesRepository(_context);
            MedicalTreatmentRepository = new MedicalTreatmentRepository(_context);
            TherapyRepository = new TherapyRepository(_context);
            MedicamentTherapyRepository = new MedicamentTherapyRepository(_context);
            BloodUnitTherapyRepository = new BloodUnitTherapyRepository(_context);
            MedicamentRepository = new MedicamentRepository(_context);
            VacationRequestsRepository = new VacationRequestsRepository(_context);
            EquipmentRepository = new EquipmentRepository(_context);
            RelocationRepository = new RelocationRepository(_context);
            BloodUnitRepository = new BloodUnitRepository(_context);
            BloodExpenditureRepository = new BloodExpenditureRepository(_context);
            BloodAcquisitionRepository = new BloodAcquisitionRepository(_context);
            ApplicationUserRepository = new ApplicationUserRepository(_context);
            RenovationRepository = new RenovationRepository(_context);
            ConsiliumRepository = new ConsiliumRepository(_context);
            DoctorScheduleRepository = new DoctorScheduleRepository(_context);
            PrescriptionRepository = new PrescriptionRepository(_context);
            SymptomRepository = new SymptomRepository(_context);
            AnamnesisRepository = new AnamnesisRepository(_context);
            RenovationEventRepository = new RenovationEventRepository(_context);

            ApplicationPatientRepository = new ApplicationPatientRepository(_context);
            ApplicationDoctorRepository = new ApplicationDoctorRepository(_context);
            AppointmentSchedulingRootRepository = new AppointmentSchedulingRootRepository(_context);
            BloodAdditionRepository = new BloodAdditionRepository(_context);
        }

        public IUserRepository UserRepository { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set; }
        public IRoomRepository RoomRepository { get; set; }
        public IFloorRepository FloorRepository { get; set; }
        public IBuildingRepository BuildingRepository { get; set; }
        public IMapRepository MapRepository { get; set; }
        public IWorkingHoursRepository WorkingHoursRepository { get; set; }
        public IAppointmentRepository AppointmentRepository { get; set; }
        public IAllergiesRepository AllergiesRepository { get; set; }
        public IMedicalTreatmentRepository MedicalTreatmentRepository { get; set; }
        public ITherapyRepository TherapyRepository { get; set; }
        public IMedicamentTherapyRepository MedicamentTherapyRepository { get; set; }
        public IBloodUnitTherapyRepository BloodUnitTherapyRepository { get; set; }
        public IMedicamentRepository MedicamentRepository { get; set; }
        public IVacationRequestsRepository VacationRequestsRepository { get; set; }
        public IEquipmentRepository EquipmentRepository { get; set; }
        public IRelocationRepository RelocationRepository { get; set; }
        public IBloodUnitRepository BloodUnitRepository { get; set; }
        public IBloodExpenditureRepository BloodExpenditureRepository { get; set; }
        public IBloodAcquisitionRepository BloodAcquisitionRepository { get; set; }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        public IApplicationPatientRepository ApplicationPatientRepository { get; set; }
        public IApplicationDoctorRepository ApplicationDoctorRepository { get; set; }
        public IRenovationRepository RenovationRepository { get; set; }
        public IConsiliumRepository ConsiliumRepository { get; set; }
        public IDoctorScheduleRepository DoctorScheduleRepository { get; set; }

        public IPrescriptionRepository PrescriptionRepository { get; set; }

        public ISymptomRepository SymptomRepository { get; set; }

        public IAnamnesisRepository AnamnesisRepository { get; set; }
        public IRenovationEventRepository RenovationEventRepository { get; set; }
        public IAppointmentSchedulingRootRepository AppointmentSchedulingRootRepository { get; set; }

        public IBloodAdditionRepository BloodAdditionRepository { get; set; }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            string type = typeof(TEntity).Name;

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
                Type repositoryType = typeof(BaseRepository<>);
                _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
                return (IBaseRepository<TEntity>)_repositories[type];

            }
            else if (_repositories.ContainsKey(type))
            {
                return (IBaseRepository<TEntity>)_repositories[type];
            }

            return null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
