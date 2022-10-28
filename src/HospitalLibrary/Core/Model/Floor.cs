namespace HospitalLibrary.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Floor : Entity
    {
        public int Number { get; set; }
        public string Purpose { get; set; }
        public Building Building { get; set; }

        public Floor() {}
    }
}
