﻿namespace IntegrationLibrary.UrgentBloodTransfer
{
    using IntegrationLibrary.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnit : Entity
    {
        public BloodType BloodType { get; set; }
        public int Amount { get; set; }

        public BloodUnit() { }

        public BloodUnit(BloodType bloodType, int amount)
        {
            BloodType = bloodType;
            Amount = amount;
        }
    }
}
