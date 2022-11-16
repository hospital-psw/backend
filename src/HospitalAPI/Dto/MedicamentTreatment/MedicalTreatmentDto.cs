namespace HospitalAPI.Dto.MedicamentTreatment
{
    using HospitalAPI.Dto.Therapy;
    using System;
    using System.Collections.Generic;

    public class MedicalTreatmentDto
    {

        public int Id { get; set; }

        public RoomDto Room { get; set; }

        public PatientDto Patient { get; set; }

        public DoctorDto Doctor { get; set; }

        public List<MedicamentTherapyDto> MedicamentTherapies { get; set; } = new List<MedicamentTherapyDto>();

        public List<BloodUnitTherapyDto> BloodUnitTherapies { get; set; } = new List<BloodUnitTherapyDto>();

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool Active { get; set; }

        public string AdmittanceReason { get; set; }

        public string Report { get; set; }
    }
}
