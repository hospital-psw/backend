namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class RescheduleAppointmentMapper
    {
        public static Appointment EntityDtoToEntity(RescheduleAppointmentDto dto)
        {
            Appointment appointment = new Appointment();

            appointment.Id = dto.Id;
            appointment.date = dto.Date;
            appointment.Duration = dto.Duration;
            appointment.ExamType = dto.ExamType;

            //appointment.Room.Id = dto.Room.Id;
            //appointment.Room.Number = dto.Room.Number;
            //appointment.Room.Floor = dto.Room.Floor;

            return appointment;
        }
    }
}
