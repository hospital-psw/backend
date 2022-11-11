namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
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
            DoctorRepository = new DoctorRepository(_context);
            FeedbackRepository = new FeedbackRepository(_context);
            AppointmentRepository = new AppointmentRepository(_context);
            PatientRepository = new PatientRepository(_context);
            MedicalTreatmentRepository = new MedicalTreatmentRepository(_context);
            TherapyRepository = new TherapyRepository(_context);
            MedicamentTherapyRepository = new MedicamentTherapyRepository(_context);
            BloodUnitTherapyRepository = new BloodUnitTherapyRepository(_context);
            MedicamentRepository = new MedicamentRepository(_context);
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
        public IMedicalTreatmentRepository MedicalTreatmentRepository { get; set; }
        public ITherapyRepository TherapyRepository { get; set; }
        public IMedicamentTherapyRepository MedicamentTherapyRepository { get; set; }
        public IBloodUnitTherapyRepository BloodUnitTherapyRepository { get; set; }
        public IMedicamentRepository MedicamentRepository { get; set; }

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
