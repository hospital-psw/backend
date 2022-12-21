namespace HospitalAPI.Mappers.Consilium
{
    using HospitalAPI.Dto.Consilium;
    using HospitalLibrary.Core.Model;
    public class DisplayConsiliumMapper
    {
        public static DisplayConsiliumDto EntityToEntityDto(Consilium consilium)
        {
            DisplayConsiliumDto consiliumDto = new DisplayConsiliumDto();

            consiliumDto.Id = consilium.Id;
            consiliumDto.Duration = consilium.Duration;
            consiliumDto.Topic = consilium.Topic.Content;
            consiliumDto.DateTime = consilium.DateTime;
            consiliumDto.Room = RoomMapper.EntityToEntityDto(consilium.Room);

            return consiliumDto;
        }
    }
}
