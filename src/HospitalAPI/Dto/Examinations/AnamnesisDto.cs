namespace HospitalAPI.Dto.Examinations
{
    using HospitalLibrary.Core.Model.Examinations;
    using System.Collections.Generic;

    public class AnamnesisDto
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public List<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();

        public List<SymptomDto> Symptoms { get; set; } = new List<SymptomDto>();

        public AppointmentDto Appointment { get; set; }

        public AnamnesisDto() { }

        public AnamnesisDto(int id, string description, List<PrescriptionDto> prescriptions, List<SymptomDto> symptoms, AppointmentDto appointment)
        {
            Id = id;
            Description = description;
            Prescriptions = prescriptions;
            Symptoms = symptoms;
            Appointment = appointment;
        }
    }
}
