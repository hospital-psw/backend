namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class RescheduleAppointmentMapper
    {
        public static Appointment EntityDtoToEntity(RescheduleAppointmentDto dto, Appointment oldAppointment)
        {
            oldAppointment.Date = dto.Date;

            return oldAppointment;
        }
    }
}
