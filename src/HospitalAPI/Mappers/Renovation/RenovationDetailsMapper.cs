namespace HospitalAPI.Mappers.Renovation
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RenovationDetailsMapper
    {
        public static RenovationDetails EntityDtoToEntity(RenovationDetailsDto dto)
        {
            RenovationDetails renovationDetails = RenovationDetails.Create(dto.NewRoomName, dto.NewRoomPurpose, dto.NewCapacity);

            return renovationDetails;
        }
    }
}
