namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDoctorScheduleService
    {
        DoctorSchedule Get(int doctorScheduleId);
        IEnumerable<DoctorSchedule> GetAll();

        IEnumerable<RecommendedAppointmentDto> RecommendAppointments(RecommendRequestDto dto);
        IEnumerable<RecommendAppointmentWithDoctorDto> RecommendAppointmentsInDateRange(int doctorId, int patientId, DateTime fromDate, DateTime toDate);
    }
}
