namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.DTO.FeedBack;
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

        public Feedback(NewFeedbackDTO dto) 
        {
            Message = dto.Message;
            Anonymous = dto.Anonymous;
            Public = dto.Public;
        }
    }
}
