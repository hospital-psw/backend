namespace HospitalAPI.Mappers.Renovation
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public class RenovationRequestMapper
    {
        public static RenovationRequest EntityDtoToEntity(RenovationRequestDto dto, List<Room> Rooms)
        {
            RenovationRequest renovationRequest = new RenovationRequest();


            renovationRequest.RenovationType = dto.RenovationType;
            renovationRequest.Rooms = Rooms;
            renovationRequest.StartTime = dto.StartTime;
            renovationRequest.Duration = dto.Duration;
            renovationRequest.RenovationDetails = new List<RenovationDetails>();
            foreach (RenovationDetailsDto dto2 in dto.RenovationDetails)
            {
                renovationRequest.RenovationDetails.Add(RenovationDetailsMapper.EntityDtoToEntity(dto2));
            }

            return renovationRequest;
        }
    }
}
