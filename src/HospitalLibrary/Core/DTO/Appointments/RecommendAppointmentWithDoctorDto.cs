namespace HospitalLibrary.Core.DTO.Appointments
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RecommendAppointmentWithDoctorDto
    {
       public RecommendedAppointmentDto recommendedAppointmentDto { get; set; }
       public string applicationDoctorName { get; set; }
       public string applicationDoctorSurname { get; set; }

        public RecommendAppointmentWithDoctorDto(RecommendedAppointmentDto recommendedAppointmentDto, string name,string surname)
        {
            this.recommendedAppointmentDto = recommendedAppointmentDto;
            this.applicationDoctorName = name;
            this.applicationDoctorSurname = surname;
        }

        public RecommendAppointmentWithDoctorDto()
        {
        }
    }
}
