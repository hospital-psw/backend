namespace HospitalAPI.Dto.Therapy
{
    using HospitalAPI.Dto.BloodUnit;

    public class BloodUnitTherapyDto : TherapyDto
    {

        public BloodUnitDto BloodUnit { get; set; }

        public int Amount { get; set; }

    }
}
