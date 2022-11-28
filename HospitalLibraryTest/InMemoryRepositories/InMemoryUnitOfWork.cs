namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Core.Repository.Core;
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
            DoctorRepository = new InMemoryDoctorRepository();
        }

        public IUserRepository UserRepository { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set; }
        public IRoomRepository RoomRepository { get; set; }
        public IFloorRepository FloorRepository { get; set; }
        public IBuildingRepository BuildingRepository { get; set; }
        public IMapRepository MapRepository { get; set; }
        public IWorkingHoursRepository WorkingHoursRepository { get; set; }
        public IAppointmentRepository AppointmentRepository { get; set; }
        public IDoctorRepository DoctorRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }
        public IAllergiesRepository AllergiesRepository { get; set; }
        public IMedicalTreatmentRepository MedicalTreatmentRepository { get; set; }
        public ITherapyRepository TherapyRepository { get; set; }
        public IMedicamentTherapyRepository MedicamentTherapyRepository { get; set; }
        public IBloodUnitTherapyRepository BloodUnitTherapyRepository { get; set; }

        public IMedicamentRepository MedicamentRepository => throw new NotImplementedException();

        public IVacationRequestsRepository VacationRequestsRepository => throw new NotImplementedException();

        public IEquipmentRepository EquipmentRepository { get; set; }

        public IRelocationRepository RelocationRepository { get; set; }

        public IBloodUnitRepository BloodUnitRepository => throw new NotImplementedException();

        public IBloodExpenditureRepository BloodExpenditureRepository => throw new NotImplementedException();

        public IBloodAcquisitionRepository BloodAcquisitionRepository => throw new NotImplementedException();

        public IApplicationUserRepository ApplicationUserRepository => throw new NotImplementedException();

        public IApplicationPatientRepository ApplicationPatientRepository => throw new NotImplementedException();

        public IApplicationDoctorRepository ApplicationDoctorRepository => throw new NotImplementedException();

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
