namespace HospitalAPI.Dto.BloodUnit
{
    using HospitalLibrary.Core.Model.Blood.Enums;

    public class BloodUnitDto
    {
        public int Id { get; set; }

        public BloodType BloodType { get; set; }

        public int Amount { get; set; }
    }
}
