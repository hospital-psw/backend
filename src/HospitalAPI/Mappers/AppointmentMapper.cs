namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers.AppUsers;
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public class AppointmentMapper
    {
        public static AppointmentDto EntityToEntityDto(Appointment appointment)
        {
            AppointmentDto dto = new AppointmentDto();

            dto.Id = appointment.Id;
            dto.Date = appointment.Date;
            dto.EndDate = appointment.Date.AddMinutes(appointment.Duration);
            dto.Duration = appointment.Duration;
            dto.IsDone = appointment.IsDone;
            dto.ExamType = appointment.ExamType;
            dto.Patient = ApplicationPatientMapper.EntityToEntityDTO(appointment.Patient);
            dto.Doctor = ApplicationDoctorMapper.EntityToEntityDTO(appointment.Doctor);
            dto.Room = RoomMapper.EntityToEntityDto(appointment.Room);
            dto.Deleted = appointment.Deleted;

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
