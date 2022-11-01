namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

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
            dto.Patient = PatientMapper.EntityToEntityDto(appointment.Patient);
            dto.Doctor = DoctorMapper.EntityToEntityDto(appointment.Doctor);
            dto.Room = RoomMapper.EntityToEntityDto(appointment.Room);

            return dto;
        }

        public static List<AppointmentDto> EntityListToEntityDtoList(List<Appointment> appointments)
        {
            List<AppointmentDto> appointmentDtos = new List<AppointmentDto>();
            appointments.ForEach(x => appointmentDtos.Add(EntityToEntityDto(x)));
            return appointmentDtos;
        }
    }
}
