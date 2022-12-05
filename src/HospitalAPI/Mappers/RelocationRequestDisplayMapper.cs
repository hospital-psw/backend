namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RelocationRequestDisplayMapper
    {
        public static RelocationRequestDisplayDto EntityToEntityDto(RelocationRequest relocationRequest)
        {
            RelocationRequestDisplayDto relocationRequestDisplayDto = new RelocationRequestDisplayDto();

            relocationRequestDisplayDto.Id = relocationRequest.Id;
            relocationRequestDisplayDto.FromRoomId = relocationRequest.FromRoom.Id;
            relocationRequestDisplayDto.ToRoomId = relocationRequest.ToRoom.Id;
            relocationRequestDisplayDto.FromRoomNumber = relocationRequest.FromRoom.Number;
            relocationRequestDisplayDto.ToRoomNumber = relocationRequest.ToRoom.Number;
            relocationRequestDisplayDto.EquipmentType = relocationRequest.Equipment.EquipmentType;
            relocationRequestDisplayDto.Quantity = relocationRequest.Quantity;
            relocationRequestDisplayDto.StartTime = relocationRequest.StartTime;
            relocationRequestDisplayDto.Duration = relocationRequest.Duration;

            return relocationRequestDisplayDto;
        }
    }
}
