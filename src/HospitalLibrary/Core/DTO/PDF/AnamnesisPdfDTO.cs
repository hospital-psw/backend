namespace HospitalLibrary.Core.DTO.PDF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnamnesisPdfDTO
    {
        public int AppointmentId { get; set; }
        public bool AreSymptomsSelected { get; set; }
        public bool AreRecepiesSelected { get; set; }
        public bool IsDescriptionSelected { get; set; }

        public AnamnesisPdfDTO() { }

        public AnamnesisPdfDTO(int appointmentId, bool areSymptomsSelected, bool areRecepiesSelected, bool isDescriptionSelected)
        {
            AppointmentId = appointmentId;
            AreSymptomsSelected = areSymptomsSelected;
            AreRecepiesSelected = areRecepiesSelected;
            IsDescriptionSelected = isDescriptionSelected;
        }
    }
}
