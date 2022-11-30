namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;
        private Dictionary<string, dynamic> _repositories;

        public InMemoryUnitOfWork()
        {
            RoomRepository = new InMemoryRoomRepository();
            AppointmentRepository = new InMemoryAppointmentRepository();
            EquipmentRepository = new InMemoryEquipmentRepository();
            ApplicationDoctorRepository = new InMemoryDoctorRepository();
            ApplicationUserRepository = new InMemoryApplicationUserRepository();
            VacationRequestsRepository = new InMemoryVacationRequestsRepository();
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

        public IMedicamentRepository MedicamentRepository => throw new NotImplementedException();

        public IVacationRequestsRepository VacationRequestsRepository { get; set; }

        public IEquipmentRepository EquipmentRepository { get; set; }

        public IRelocationRepository RelocationRepository { get; set; }

        public IBloodUnitRepository BloodUnitRepository => throw new NotImplementedException();

        public IBloodExpenditureRepository BloodExpenditureRepository => throw new NotImplementedException();

        public IBloodAcquisitionRepository BloodAcquisitionRepository => throw new NotImplementedException();

        public IApplicationUserRepository ApplicationUserRepository { get; set; }

        public IApplicationPatientRepository ApplicationPatientRepository => throw new NotImplementedException();

        public IApplicationDoctorRepository ApplicationDoctorRepository { get; set; }
        public IConsiliumRepository ConsiliumRepository { get; set; }
        public IDoctorScheduleRepository DoctorScheduleRepository { get; set; }

        public IPrescriptionRepository PrescriptionRepository { get; set; }

        public ISymptomRepository SymptomRepository { get; set; }

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
