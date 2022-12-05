namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class ConsiliumDisplayMapper
    {
        public static ConsiliumDisplayDto EntityToEntityDto(Consilium consilium)
        {
            ConsiliumDisplayDto consiliumDisplayDto = new ConsiliumDisplayDto();

            consiliumDisplayDto.Duration = consilium.Duration;
            consiliumDisplayDto.DateTime = consilium.DateTime;
            consiliumDisplayDto.Topic = consilium.Topic;

            return consiliumDisplayDto;
        }
    }
}
