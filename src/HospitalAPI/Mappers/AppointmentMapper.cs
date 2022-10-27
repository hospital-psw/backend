namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class AppointmentMapper
    {
        public static AppointmentDto EntityToEntityDto(Appointment appointment)
        {
            AppointmentDto dto = new AppointmentDto();

            dto.Id = appointment.Id;
            dto.Date = appointment.Date;
            dto.Duration = appointment.Duration;
            dto.IsDone = appointment.IsDone;
            dto.ExamType = appointment.ExamType;

            //dto.Room.Id = appointment.Room.Id;
            //dto.Room.Number = appointment.Room.Number;
            //dto.Room.Floor = appointment.Room.Floor;

            return dto;
        }
    }
}
