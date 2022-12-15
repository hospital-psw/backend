namespace IntegrationServices.ReportService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnit
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

