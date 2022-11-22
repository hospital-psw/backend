namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class EquipmentDto
    {
        public int Id { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public int Quantity { get; set; }
        public RoomDto Room { get; set; }
        public int ReservedQuantity { get; set; }

        public EquipmentDto() { }

        public EquipmentDto(int id, EquipmentType equipmentType, int quantity, int reservedQuantity)
        {
            Id = id;
            EquipmentType = equipmentType;
            Quantity = quantity;
            ReservedQuantity = reservedQuantity;
        }
    }
}
