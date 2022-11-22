namespace HospitalLibrary.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Allergies : Entity
    {
        public String Name { get; set; }

        public Allergies(string name)
        {
            Name = name;
        }

        public Allergies()
        {
        }
    }
}
