﻿namespace IntegrationLibrary.UrgentBloodTransfer
{
    using grpcServices;
    using IntegrationLibrary.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UrgentBloodTransfer : Entity
    {
        public grpcServices.BloodType BloodType { get; set; }
        public uint Amount { get; set; }

        public UrgentBloodTransfer(grpcServices.BloodType bloodType, uint amount)
        {
            if (Validate(amount))
            {
                BloodType = bloodType;
                Amount = amount;
            }
            else
            {
                throw new Exception("Cannot create Urgent Blood Transfer object. Amout passed is less than 1");
            }
        }

        private bool Validate(uint amount)
        {
            return amount >= 1;
        }

        public override bool Equals(object obj)
        {
            var other = obj as UrgentBloodTransfer;
            return (this.Amount == other.Amount) && (this.BloodType == other.BloodType);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}