namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class UpdateDoctorDto : UpdateUserDto
    {
        public Specialization Specialization { get; set; }
    }
}
