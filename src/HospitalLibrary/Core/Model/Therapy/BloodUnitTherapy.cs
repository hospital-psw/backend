namespace HospitalLibrary.Core.Model.Therapy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitTherapy : Therapy
    {

        //public BloodUnit BloodUnit { get; set; }

        public int AmountOfBloodUnit { get; set; }

        public BloodUnitTherapy()
        {

        }

        public BloodUnitTherapy(int amountOfBloodUnit)
        {
            //BloodUnit = bloodUnit
            AmountOfBloodUnit = amountOfBloodUnit;
        }
    }
}
