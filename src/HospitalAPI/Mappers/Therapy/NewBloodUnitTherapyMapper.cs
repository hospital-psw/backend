namespace HospitalAPI.Mappers.Therapy
{
    using HospitalAPI.Dto.Therapy;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Therapy;
    using System;

    public class NewBloodUnitTherapyMapper
    {
        public static BloodUnitTherapy EntityDtoToEntity(NewBloodUnitTherapyDto dto)
        {
            return new BloodUnitTherapy
            {
                //BloodUnit = dto.BloodUnit
                About = dto.About,
                AmountOfBloodUnit = dto.Amount,
                Type = TherapyType.BLOOD_UNIT,
                Start = DateTime.Now,
                End = default(DateTime)
            };
        }
    }
}
