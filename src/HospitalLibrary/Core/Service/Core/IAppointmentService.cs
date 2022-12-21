namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        Appointment Get(int id);
        Appointment Update(Appointment appointment);
        IEnumerable<Appointment> GetAll();

        Appointment Create(NewAppointmentDto dto);

        void Delete(Appointment appointment);

        IEnumerable<Appointment> GetByDoctorsId(int doctorId);

        IEnumerable<Appointment> GetAppointmentsInDateRangeDoctor(int doctorId, DateTime from, DateTime to);

        List<Appointment> GetAllForRoom(int roomId);

        IEnumerable<Appointment> GetAllBySpecialization(Specialization specialization, DateRange dateRange);

        IEnumerable<ReccomendedBySpecializationDTO> RecommendAppointmentsBySpecialization(ReccomendBySpecializationRequestDto dto, Specialization specialization);
    }

}
