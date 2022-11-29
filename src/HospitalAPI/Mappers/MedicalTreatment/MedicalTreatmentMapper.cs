namespace HospitalAPI.Mappers.MedicalTreatment
{
    using HospitalAPI.Dto.MedicamentTreatment;
    using HospitalAPI.Mappers.Therapy;
    using HospitalLibrary.Core.Model.MedicalTreatment;

    public class MedicalTreatmentMapper
    {
        public static MedicalTreatmentDto EntityToEntityDto(MedicalTreatment medicalTreatment)
        {
            MedicalTreatmentDto dto = new MedicalTreatmentDto();

            dto.Id = medicalTreatment.Id;
            dto.Room = RoomMapper.EntityToEntityDto(medicalTreatment.Room);
            //dto.Patient = PatientMapper.EntityToEntityDto(medicalTreatment.Patient);
            //dto.Doctor = DoctorMapper.EntityToEntityDto(medicalTreatment.Doctor);

            medicalTreatment.MedicamentTherapies.ForEach(t => dto.MedicamentTherapies.Add(MedicamentTherapyMapper.EntityToEntityDto(t)));
            medicalTreatment.BloodUnitTherapies.ForEach(t => dto.BloodUnitTherapies.Add(BloodUnitTherapyMapper.EntityToEntityDto(t)));

            dto.Start = medicalTreatment.Start;
            dto.End = medicalTreatment.End;
            dto.Active = medicalTreatment.Active;
            dto.AdmittanceReason = medicalTreatment.AdmittanceReason;
            dto.Report = medicalTreatment.Report;

            return dto;
        }
    }
}
