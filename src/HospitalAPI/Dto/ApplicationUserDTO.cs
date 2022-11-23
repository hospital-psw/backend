namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? ClientURI { get; set; }

    }
}
