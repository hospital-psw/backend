namespace HospitalLibrary.Core.DTO.VacationRequest
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewVacationRequestDto
    {
        public int DoctorId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public VacationRequestStatus Status { get; set; }
        public string Comment { get; set; }
        public bool Urgent { get; set; }

    }
}
