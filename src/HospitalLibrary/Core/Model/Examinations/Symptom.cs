namespace HospitalLibrary.Core.Model.Examinations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Symptom : Entity
    {
        public string Name { get; set; }

        public Symptom(string name)
        {
            Name = name;
        }

        public Symptom() { }
    }
}
