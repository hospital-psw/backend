namespace HospitalLibrary.Core.Model.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HospitalLibrary.Core.Model;

    public class Medicament : Entity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public List<Allergies> Allergens { get; set; } = new List<Allergies>();

        public Medicament() { }
        public Medicament(string name, string description, int quantity)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
        }

    }
}
