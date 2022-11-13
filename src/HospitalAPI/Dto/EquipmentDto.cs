namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class EquipmentDto
    {
        public int Id;
        public EquipmentType EquipmentType;
        public int Quantity;

        public EquipmentDto() { }

        public EquipmentDto(int id, EquipmentType equipmentType, int quantity)
        {
            Id = id;
            EquipmentType = equipmentType;
            Quantity = quantity;
        }
    }
}
