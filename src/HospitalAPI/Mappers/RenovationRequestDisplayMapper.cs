namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;

    public class RenovationRequestDisplayMapper
    {

        public static RenovationRequest EntityDtoToEntity(RenovationRequestDto dto, List<Room> rooms)
        {
            try
            {
                RenovationRequest renovationRequest = RenovationRequest.Create(dto.RenovationType, rooms, dto.StartTime, dto.Duration, RenovationDetailsDtoToEntity(dto.RenovationDetails));
                return renovationRequest;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static RenovationRequestDisplayDto EntityToEntityDto(RenovationRequest renovationRequest)
        {
            RenovationRequestDisplayDto renovationRequestDisplayDto = new RenovationRequestDisplayDto();

            renovationRequestDisplayDto.Id = renovationRequest.Id;
            renovationRequestDisplayDto.renovationType = renovationRequest.RenovationType;
            renovationRequestDisplayDto.StartTime = renovationRequest.StartTime;
            renovationRequestDisplayDto.Duration = renovationRequest.Duration;

            return renovationRequestDisplayDto;
        }

        public static List<RenovationDetails> RenovationDetailsDtoToEntity(List<RenovationDetailsDto> renovationDetailsDto)
        {
            try
            {
                List<RenovationDetails> details = new List<RenovationDetails>();
                foreach (RenovationDetailsDto detail in renovationDetailsDto)
                {
                    details.Add(RenovationDetails.Create(detail.NewRoomName, detail.NewRoomPurpose, detail.NewCapacity));
                }
                return details;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
