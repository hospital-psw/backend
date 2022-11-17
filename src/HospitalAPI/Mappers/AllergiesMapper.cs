namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class AllergiesMapper
    {
        public static AllergiesDto EntityToEntityDto(Allergies allergy)
        {
            AllergiesDto dto = new AllergiesDto();

            dto.Id = allergy.Id;
            dto.Name = allergy.Name;
            

            return dto;
        }

        public static Allergies EntityDtoToEntity(AllergiesDto dto)
        {
            Allergies allergy = new Allergies();

            allergy.Id = dto.Id;
            allergy.Name = dto.Name;
           

            return allergy;
        }
    }
}
