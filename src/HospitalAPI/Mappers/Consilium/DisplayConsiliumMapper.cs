namespace HospitalAPI.Mappers.Consilium
{
    using HospitalAPI.Dto.Consilium;
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public class DisplayConsiliumMapper
    {
        public static DisplayConsiliumDto EntityToEntityDto(Consilium consilium)
        {
            DisplayConsiliumDto consiliumDto = new DisplayConsiliumDto();
            if (consilium == null) return null;

            consiliumDto.Id = consilium.Id;
            consiliumDto.Duration = consilium.Duration;
            consiliumDto.Topic = consilium.Topic.Content;
            consiliumDto.StartDateTime = consilium.DateTime;
            consiliumDto.EndDateTime = consilium.DateTime.AddMinutes(consilium.Duration);
            consiliumDto.Room = RoomMapper.EntityToEntityDto(consilium.Room);

            return consiliumDto;
        }

        public static List<DisplayConsiliumDto> EntityListToDtoList(List<Consilium> consiliums)
        {
            List<DisplayConsiliumDto> returnList = new List<DisplayConsiliumDto>();
            if (consiliums == null) return returnList;
            consiliums.ForEach(x => returnList.Add(EntityToEntityDto(x)));
            return returnList;
        }
    }
}
