namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Feedback : Entity
    {
        public ApplicationPatient Creator { get; set; }

        public FeedbackMessage Message { get; set; }

        public bool Anonymous { get; set; }

        public bool Public { get; set; }

        public FeedbackStatus Status { get; set; }

        public Feedback()
        {
        }

        public Feedback(NewFeedbackDTO dto)
        {
            Message = FeedbackMessage.Create(dto.Message);
            Anonymous = dto.Anonymous;
            Public = dto.Public;
        }
    }
}
