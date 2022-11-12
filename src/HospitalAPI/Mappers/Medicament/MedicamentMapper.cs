namespace HospitalAPI.Mappers.Medicament
{
    using HospitalAPI.Dto.Medicament;
    using HospitalLibrary.Core.Model.Medicament;

    public class MedicamentMapper
    {
        public static MedicamentDto EntityToEntityDto(Medicament medicament)
        {
            MedicamentDto dto = new MedicamentDto();

            dto.Id = medicament.Id;
            dto.Name = medicament.Name;
            dto.Quantity = medicament.Quantity;
            dto.Description = medicament.Description;
            //dto.Allergens = medicament.Allergens;

            return dto;
        }
    }
}
