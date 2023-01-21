namespace HospitalAPI.Mappers.Examinations
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Examinations;
    using HospitalLibrary.Core.Model.Examinations;
    using System.Collections.Generic;

    public class AnamnesisMapper
    {
        public static AnamnesisDto EntityToEntityDto(Anamnesis anamnesis)
        {
            if (anamnesis == null) return null;

            AnamnesisDto dto = new AnamnesisDto();
            AppointmentDto appointmentDto = AppointmentMapper.EntityToEntityDto(anamnesis.Appointment);
            List<PrescriptionDto> prescriptions = PrescriptionMapper.EntityListToEntityDtoList(anamnesis.Prescriptions);
            List<SymptomDto> symptoms = SymptomMapper.EntityListToEntityDtoList(anamnesis.Symptoms);

            dto.Id = anamnesis.Id;
            dto.Description = anamnesis.Description;
            dto.Prescriptions = prescriptions;
            dto.Symptoms = symptoms;
            dto.Appointment = appointmentDto;

            return dto;
        }

        public static List<AnamnesisDto> EntityListToEntityDtoList(List<Anamnesis> anamneses)
        {
            List<AnamnesisDto> list = new List<AnamnesisDto>();
            anamneses.ForEach(x => list.Add(EntityToEntityDto(x)));
            return list;
        }
    }
}
