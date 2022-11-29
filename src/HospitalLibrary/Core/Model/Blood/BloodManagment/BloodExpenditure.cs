namespace HospitalLibrary.Core.Model.Blood.BloodManagment
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodExpenditure : Entity
    {
        public ApplicationDoctor Doctor{ get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }



        public BloodExpenditure() { }

        public BloodExpenditure(ApplicationDoctor doctor, BloodType bloodType, int amount, string reason, DateTime date)
        {
            Doctor = doctor;
            BloodType = bloodType;
            Amount = amount;
            Reason = reason;
            Date = date;
        }
    }
}
