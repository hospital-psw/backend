namespace HospitalLibrary.Core.Model.Blood
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAddition : Entity
    {
        public DateTime Date { get; set; }
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }

        public BloodAddition() { }

        public BloodAddition(DateTime date, BloodType bloodType, int amount)
        {
            Date = date;
            BloodType = bloodType;
            Amount = amount;
        }



    }
}
