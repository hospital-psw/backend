namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class BuildingMapper
    {
        public static BuildingDto EntityToEntityDto(Building building)
        {
            BuildingDto dto = new BuildingDto();

            dto.Id = building.Id;
            dto.Name = building.Name;
            dto.Address = building.Address;

            return dto;
        }

        public static Building EntityDtoToEntity(BuildingDto dto)
        {
            Building building = new Building();

            building.Id = dto.Id;
            building.Name = dto.Name;
            building.Address = dto.Address;

            return building;
        }
    }
}
