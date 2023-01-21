namespace HospitalLibrary.Core.DTO.BloodManagment
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateAcquisitionDTO
    {

        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }


        public CreateAcquisitionDTO() { }

        public CreateAcquisitionDTO(int doctorId, DateTime date, BloodType bloodType, int amount, string reason, BloodRequestStatus status)
        {
            DoctorId = doctorId;
            Date = date;
            BloodType = bloodType;
            Amount = amount;
            Reason = reason;
        }
    }
}
