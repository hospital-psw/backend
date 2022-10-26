namespace HospitalLibrary.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NewFeedbackDTO
    {
        public int CreatorId { get; set; }

        public string Message { get; set; }

        public bool Anonymous { get; set; }

        public bool Public { get; set; }

    }
}
