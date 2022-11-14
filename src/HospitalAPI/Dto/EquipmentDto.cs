namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class EquipmentDto
    {
        public int Id { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public int Quantity { get; set; }
        public RoomDto Room { get; set; }

        public EquipmentDto() { }

    }
}
