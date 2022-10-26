namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class NewDoctorDto : NewUserDto
    {
        public Specialization Specialization { get; set; }
    }
}
