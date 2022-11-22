namespace HospitalAPI.Dto.Medicament
{
    using System.Collections.Generic;

    public class MedicamentDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public List<AllergiesDto> Allergens { get; set; } = new List<AllergiesDto>();
    }
}
