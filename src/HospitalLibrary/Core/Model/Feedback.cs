namespace HospitalLibrary.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Feedback : Entity
    {
        public User Creator { get; set; }

        public string Message { get; set; }

        public bool Anonymous { get; set; }

        public bool Public { get; set; }

        public Feedback() 
        {
        }
    }
}
