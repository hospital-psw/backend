namespace HospitalAPI.Mappers.Consilium
{
    using HospitalAPI.Dto.Consilium;
    using HospitalAPI.Mappers.AppUsers;
    using HospitalLibrary.Core.Model;

    public class ConsiliumMapper
    {
        public static ConsiliumDto EntityToDto(Consilium consilium)
        {
            ConsiliumDto consiliumDto = new ConsiliumDto();

            consiliumDto.Id = consilium.Id;
            consiliumDto.Duration = consilium.Duration;
            consiliumDto.Topic = consilium.Topic;
            consiliumDto.DateTime = consilium.DateTime;
            consilium.DoctorsSchedule.ForEach(ds => consiliumDto.Doctors.Add(ApplicationDoctorMapper.EntityToEntityDTO(ds.Doctor)));

            return consiliumDto;
        }
    }
}
