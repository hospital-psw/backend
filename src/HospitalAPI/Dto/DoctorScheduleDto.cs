namespace HospitalAPI.Dto
{
    using HospitalAPI.Dto.Consilium;
    using System.Collections.Generic;

    public class DoctorScheduleDto
    {
        public List<DisplayConsiliumDto> Consiliums { get; set; }

        public List<AppointmentDto> Appointments { get; set; }

        public List<VacationRequestDto> Vacations { get; set; }


    }
}
