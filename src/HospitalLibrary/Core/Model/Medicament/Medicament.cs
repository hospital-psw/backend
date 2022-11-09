namespace HospitalLibrary.Core.Model.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Medicament : Entity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        //public List<Allergens> Allergens { get; set; }

        public Medicament() { }
        public Medicament(string name, string description, int quantity)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
        }

    }
}
