namespace HospitalLibrary.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class User : Entity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public User() 
        {
        }
    }
}
