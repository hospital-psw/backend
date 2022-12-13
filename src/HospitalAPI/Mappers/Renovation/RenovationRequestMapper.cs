namespace HospitalAPI.Mappers.Renovation
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public class RenovationRequestMapper
    {
        public static RenovationRequest EntityDtoToEntity(RenovationRequestDto dto, List<Room> Rooms)
        {

            List<RenovationDetails> details = new List<RenovationDetails>();
            foreach (RenovationDetailsDto dto2 in dto.RenovationDetails)
            {
                details.Add(RenovationDetailsMapper.EntityDtoToEntity(dto2));
            }

            RenovationRequest renovationRequest = RenovationRequest.Create(dto.RenovationType, Rooms, dto.StartTime, dto.Duration, details);

            return renovationRequest;
        }
    }
}
