namespace HospitalAPI.Mappers.BloodUnit
{
    using HospitalAPI.Dto.BloodUnit;
    using HospitalLibrary.Core.Model.Blood;

    public class BloodUnitMapper
    {
        public static BloodUnitDto EntityToEntityDto(BloodUnit bloodUnit)
        {
            BloodUnitDto bloodUnitDto = new BloodUnitDto();

            bloodUnitDto.Id = bloodUnit.Id;
            bloodUnitDto.BloodType = bloodUnit.BloodType;
            bloodUnitDto.Amount = bloodUnit.Amount;

            return bloodUnitDto;
        }
    }
}
