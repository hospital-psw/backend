namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ValueObjects;

    public class FloorMapper
    {
        public static FloorDto EntityToEntityDto(Floor floor)
        {
            FloorDto dto = new FloorDto();

            dto.Id = floor.Id;
            dto.Number = floor.Number.Number;
            dto.Purpose = floor.Purpose;
            dto.Building = BuildingMapper.EntityToEntityDto(floor.Building);

            return dto;
        }

        public static Floor EntityDtoToEntity(FloorDto dto)
        {
            Floor floor = new Floor();

            floor.Id = dto.Id;
            floor.Number = FloorNumber.Create(dto.Number);
            floor.Purpose = dto.Purpose;
            floor.Building = BuildingMapper.EntityDtoToEntity(dto.Building);

            return floor;
        }
    }
}
