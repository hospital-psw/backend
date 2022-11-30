namespace HospitalLibrary.Core.DTO.Consilium
{
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScheduleConsiliumDto
    {
        public DateRange DateRange { get; set; }
        public string Topic { get; set; }
        public List<int> SelectedDoctors { get; set; }
        public List<Specialization> SelectedSpecializations { get; set; }
        public int Duration { get; set; }
        public int DoctorId { get; set; }
    }
}
