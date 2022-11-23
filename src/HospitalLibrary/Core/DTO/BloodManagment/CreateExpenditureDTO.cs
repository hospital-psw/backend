namespace HospitalLibrary.Core.DTO.BloodManagment
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateExpenditureDTO
    {
        public int DoctorId { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        //public DateTime Date { get; set; }

        public CreateExpenditureDTO() { }

        public CreateExpenditureDTO(int doctorId, BloodType bloodType,int amount, string reason)
        {
            DoctorId = doctorId;
            BloodType = bloodType;
            Amount = amount;
            Reason = reason;
            //Date = date;
        }


    }
}
