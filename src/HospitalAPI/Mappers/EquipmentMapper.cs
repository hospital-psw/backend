namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;


    public class EquipmentMapper
    {
        public static EquipmentDto EntityToEntityDto(EquipmentDto equipment)
        {
            EquipmentDto dto = new EquipmentDto();
            dto.EquipmentType = equipment.EquipmentType;
            dto.Quantity = equipment.Quantity;
            dto.Id = equipment.Id;
            return dto;
        }

        public static Equipment EntityDtoToEntity(EquipmentDto dto)
        {
            Equipment equipment = new Equipment();

            equipment.EquipmentType = dto.EquipmentType;
            equipment.Quantity = dto.Quantity;
            equipment.Id = dto.Id;
            return equipment;
        }
    }
}
