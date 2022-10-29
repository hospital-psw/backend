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
    }
}
