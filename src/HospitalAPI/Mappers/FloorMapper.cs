﻿namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class FloorMapper
    {
        public static FloorDto EntityToEntityDto(Floor floor)
        {
            FloorDto dto = new FloorDto();

            dto.Id = floor.Id;
            dto.Number = floor.Number;
            dto.Purpose = floor.Purpose;
            dto.Building = BuildingMapper.EntityToEntityDto(floor.Building);

            return dto;
        }
    }
}