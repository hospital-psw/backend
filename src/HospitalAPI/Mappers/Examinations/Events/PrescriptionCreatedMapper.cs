namespace HospitalAPI.Mappers.Examinations.Events
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalLibrary.Core.Model.Events;

    public class PrescriptionCreatedMapper
    {
        public static PrescriptionCreated DtoToEntity(PrescriptionCreatedDto dto)
        {
            return new PrescriptionCreated(dto.AggregateId, dto.TimeStamp, dto.EventType.ToString(), 
                                           dto.NewPrescription.MedicamentId, dto.NewPrescription.Description,
                                           dto.NewPrescription.From, dto.NewPrescription.To);
        }
    }
}
