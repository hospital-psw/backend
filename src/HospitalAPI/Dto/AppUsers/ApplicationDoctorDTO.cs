namespace HospitalAPI.Dto.AppUsers
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationDoctorDTO : ApplicationUserDTO
    {
        public Specialization Specialization { get; set; }

        //public ApplicationDoctorDTO(ApplicationDoctor doctor)
        //{
        //    Id = doctor.Id;
        //    Specialization = doctor.Specialization;
        //    FirstName = doctor.FirstName;
        //    LastName = doctor.LastName;
        //    Email = doctor.Email;
        //}
    }
}
