namespace HospitalAPI.Mappers.Therapy
{
    using HospitalAPI.Dto.Therapy;
    using HospitalAPI.Mappers.BloodUnit;
    using HospitalLibrary.Core.Model.Therapy;

    public class BloodUnitTherapyMapper
    {
        public static BloodUnitTherapyDto EntityToEntityDto(BloodUnitTherapy bloodUnitTherapy)
        {
            BloodUnitTherapyDto dto = new BloodUnitTherapyDto();

            dto.Id = bloodUnitTherapy.Id;
            dto.Start = bloodUnitTherapy.Start;
            dto.End = bloodUnitTherapy.End;
            dto.About = bloodUnitTherapy.About;
            dto.Type = bloodUnitTherapy.Type;

            dto.BloodUnit = BloodUnitMapper.EntityToEntityDto(bloodUnitTherapy.BloodUnit);
            dto.Amount = bloodUnitTherapy.AmountOfBloodUnit;

            return dto;
        }
    }
}
