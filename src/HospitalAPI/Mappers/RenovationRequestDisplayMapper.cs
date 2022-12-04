namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RenovationRequestDisplayMapper
    {
        public static RenovationRequestDisplayDto EntityToEntityDto(RenovationRequest renovationRequest)
        {
            RenovationRequestDisplayDto renovationRequestDisplayDto = new RenovationRequestDisplayDto();

            renovationRequestDisplayDto.Id = renovationRequest.Id;
            renovationRequest.RenovationType = renovationRequest.RenovationType;
            renovationRequestDisplayDto.StartTime = renovationRequest.StartTime;
            renovationRequestDisplayDto.Duration = renovationRequest.Duration;

            return renovationRequestDisplayDto;
        }
    }
}
