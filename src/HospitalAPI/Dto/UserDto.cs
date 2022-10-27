namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
