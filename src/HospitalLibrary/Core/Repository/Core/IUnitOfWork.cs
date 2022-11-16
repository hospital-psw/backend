namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Repository.Blood.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        int Save();

        void Dispose();

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        public IUserRepository UserRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }
        public IRoomRepository RoomRepository { get; }
        public IFloorRepository FloorRepository { get; }
        public IBuildingRepository BuildingRepository { get; }
        public IMapRepository MapRepository { get; }
        public IWorkingHoursRepository WorkingHoursRepository { get; }
        public IAppointmentRepository AppointmentRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IMedicalTreatmentRepository MedicalTreatmentRepository { get; }
        public ITherapyRepository TherapyRepository { get; }
        public IMedicamentTherapyRepository MedicamentTherapyRepository { get; }
        public IBloodUnitTherapyRepository BloodUnitTherapyRepository { get; }
        public IMedicamentRepository MedicamentRepository { get; }
        public IVacationRequestsRepository VacationRequestsRepository { get; }
        public IEquipmentRepository EquipmentRepository { get; }
        public IBloodUnitRepository BloodUnitRepository { get; }
        public IBloodExpenditureRepository BloodExpenditureRepository { get; }
        public IBloodAcquisitionRepository BloodAcquisitionRepository { get; }


    }
}
