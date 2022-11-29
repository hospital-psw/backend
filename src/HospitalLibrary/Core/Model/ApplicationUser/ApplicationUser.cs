namespace HospitalLibrary.Core.Model.ApplicationUser
{
    using HospitalLibrary.Core.Model.Enums;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public ApplicationUser() : base() { }
        public ApplicationUser(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public ApplicationUser(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
