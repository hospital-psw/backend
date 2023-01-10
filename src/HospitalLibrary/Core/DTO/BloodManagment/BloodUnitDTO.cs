namespace HospitalLibrary.Core.DTO.BloodManagment
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitDTO
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }

        public BloodUnitDTO() { }

        public BloodUnitDTO(BloodType bloodType, int amount)
        {
            BloodType = bloodType;
            Amount = amount;
        }
    }
}
