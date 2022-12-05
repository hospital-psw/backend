namespace HospitalAPI.Mappers.Renovation
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RenovationDetailsMapper
    {
        public static RenovationDetails EntityDtoToEntity(RenovationDetailsDto dto)
        {
            RenovationDetails renovationDetails = new RenovationDetails();

            renovationDetails.NewRoomName = dto.NewRoomName;
            renovationDetails.NewRoomPurpose = dto.NewRoomPurpose;
            renovationDetails.NewCapacity = dto.NewCapacity;

            return renovationDetails;
        }
    }
}
