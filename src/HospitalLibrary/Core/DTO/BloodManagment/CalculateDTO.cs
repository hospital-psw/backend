namespace HospitalLibrary.Core.DTO.BloodManagment
{
    public class CalculateDTO
    {
        public int APlusAmount { get; set; }
        public int BPlusAmount { get; set; }
        public int AMinusAmount { get; set; }
        public int BMinusAmount { get; set; }
        public int ABPlusAmount { get; set; }
        public int OPlusAmount { get; set; }
        public int ABMinusAmount { get; set; }
        public int OMinusAmount { get; set; }

        public int TotalSum { get; set; }

        public CalculateDTO()
        {
            ABMinusAmount = 0;
            ABPlusAmount = 0;
            OPlusAmount = 0;
            OMinusAmount = 0;
            APlusAmount = 0;
            AMinusAmount = 0;
            BPlusAmount = 0;
            BMinusAmount = 0;
            TotalSum = 0;
        }

    }
}
