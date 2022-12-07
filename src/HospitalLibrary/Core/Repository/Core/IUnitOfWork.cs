namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        int Save();
        new void Dispose();

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        public IUserRepository UserRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }
        public IRoomRepository RoomRepository { get; }
        public IFloorRepository FloorRepository { get; }
        public IBuildingRepository BuildingRepository { get; }
        public IMapRepository MapRepository { get; }
        public IWorkingHoursRepository WorkingHoursRepository { get; }
        public IAppointmentRepository AppointmentRepository { get; }
        public IAllergiesRepository AllergiesRepository { get; }
        public IMedicalTreatmentRepository MedicalTreatmentRepository { get; }
        public ITherapyRepository TherapyRepository { get; }
        public IMedicamentTherapyRepository MedicamentTherapyRepository { get; }
        public IBloodUnitTherapyRepository BloodUnitTherapyRepository { get; }
        public IMedicamentRepository MedicamentRepository { get; }
        public IVacationRequestsRepository VacationRequestsRepository { get; }
        public IEquipmentRepository EquipmentRepository { get; }
        public IRelocationRepository RelocationRepository { get; }
        public IBloodUnitRepository BloodUnitRepository { get; }
        public IBloodExpenditureRepository BloodExpenditureRepository { get; }
        public IBloodAcquisitionRepository BloodAcquisitionRepository { get; }
        public IApplicationUserRepository ApplicationUserRepository { get; }
        public IApplicationPatientRepository ApplicationPatientRepository { get; }
        public IApplicationDoctorRepository ApplicationDoctorRepository { get; }
        public IRenovationRepository RenovationRepository { get; }
        public IConsiliumRepository ConsiliumRepository { get; }
        public IDoctorScheduleRepository DoctorScheduleRepository { get; }

        public IPrescriptionRepository PrescriptionRepository { get; }

        public ISymptomRepository SymptomRepository { get; }

        public IAnamnesisRepository AnamnesisRepository { get; }

    }
}
