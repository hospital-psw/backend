namespace HospitalLibrary.Core.DTO.MedicalTreatment
{
    using HospitalLibrary.Core.DTO.Therapy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewMedicalTreatmentDto
    {

        public int RoomId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public NewMedicamentTherapyDto MedicamentTherapy { get; set; }

        public NewBloodUnitTherapyDto BloodUnitTherapy { get; set; }

    }
}
