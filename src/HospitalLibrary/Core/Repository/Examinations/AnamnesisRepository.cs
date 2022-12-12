namespace HospitalLibrary.Core.Repository.Examinations
{
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnamnesisRepository : BaseRepository<Anamnesis>, IAnamnesisRepository
    {
        public AnamnesisRepository(HospitalDbContext context) : base(context)
        {
        }

        public override IEnumerable<Anamnesis> GetAll()
        {
            return HospitalDbContext.Anamneses.Include(x => x.Appointment)
                                              .ThenInclude(x => x.Doctor)
                                              .Include(x => x.Appointment)
                                              .ThenInclude(x => x.Patient)
                                              .Include(x => x.Appointment)
                                              .ThenInclude(x => x.Room)
                                              .ThenInclude(x => x.Floor)
                                              .ThenInclude(x => x.Building)
                                              .Include(x => x.Symptoms)
                                              .Include(x => x.Prescriptions)
                                              .ThenInclude(x => x.Medicament)
                                              .Where(x => !x.Deleted);
        }

        public override Anamnesis Get(int id)
        {
            return GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Anamnesis> GetByDoctor(int doctorId)
        {
            return GetAll().Where(x => x.Appointment.Doctor.Id == doctorId);
        }

        public IEnumerable<Anamnesis> GetByPatient(int patientId)
        {
            return GetAll().Where(x => x.Appointment.Patient.Id == patientId);
        }

        public IEnumerable<Anamnesis> GetInDateRange(DateRange dateRange)
        {
            return GetAll().Where(x => dateRange.InRange(x.Appointment.Date));
        }

        public Anamnesis GetByAppointment(int id) {
            return GetAll().FirstOrDefault(x => x.Appointment.Id == id);
        }

    }
}
