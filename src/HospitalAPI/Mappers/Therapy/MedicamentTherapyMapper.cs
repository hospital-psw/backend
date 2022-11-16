namespace HospitalAPI.Mappers.Therapy
{
    using HospitalAPI.Dto.Therapy;
    using HospitalAPI.Mappers.Medicament;
    using HospitalLibrary.Core.Model.Therapy;

    public class MedicamentTherapyMapper
    {
        public static MedicamentTherapyDto EntityToEntityDto(MedicamentTherapy medicamentTherapy)
        {
            MedicamentTherapyDto dto = new MedicamentTherapyDto();

            dto.Id = medicamentTherapy.Id;
            dto.Start = medicamentTherapy.Start;
            dto.End = medicamentTherapy.End;
            dto.About = medicamentTherapy.About;
            dto.Type = medicamentTherapy.Type;

            dto.Medicament = MedicamentMapper.EntityToEntityDto(medicamentTherapy.Medicament);
            dto.Amount = medicamentTherapy.AmountOfMedicament;

            return dto;
        }
    }
}
