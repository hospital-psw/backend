namespace HospitalLibrary.Core.DTO.Appointments
{
    using HospitalLibrary.Core.Model.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReccomendBySpecializationRequestDto
    {
        public DateRange DateRange { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }
    }
}
