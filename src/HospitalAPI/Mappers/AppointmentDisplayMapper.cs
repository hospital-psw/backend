namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class AppointmentDisplayMapper
    {
        public static AppointmentDisplayDto EntityToEntityDto(Appointment appointment)
        {
            AppointmentDisplayDto dto = new AppointmentDisplayDto();
            dto.Date = appointment.Date;
            dto.Duration = appointment.Duration;
            dto.ExaminationType = appointment.ExamType;

            return dto;
        }
    }
}
