namespace HospitalLibrary.Core.DTO.BloodManagment
{
    public class CalculateDTO
    {
        public int APlusAmount { get; set; } = 0;
        public int BPlusAmount { get; set; } = 0;
        public int AMinusAmount { get; set; } = 0;
        public int BMinusAmount { get; set; } = 0;
        public int ABPlusAmount { get; set; } = 0;
        public int OPlusAmount { get; set; } = 0;
        public int ABMinusAmount { get; set; } = 0;
        public int OMinusAmount { get; set; } = 0;
        public int TotalSum { get; set; } = 0;
    }
}
