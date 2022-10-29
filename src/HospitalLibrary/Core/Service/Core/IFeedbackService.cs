namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFeedbackService
    {
        Feedback Add(NewFeedbackDTO dto);
        IEnumerable<Feedback> GetAll();
        Feedback Get(int id);
        IEnumerable<Feedback> GetAllPublicFeedback();
        IEnumerable<Feedback> GetAllPrivateFeedback();
        IEnumerable<Feedback> GetAllAnonymousFeedback();
        IEnumerable<Feedback> GetAllIdentifiedFeedback();
        bool MakePublic(int id);
        bool MakePrivate(int id);
        bool MakeAnonymous(int id);
        bool MakeIdentified(int id);
        public List<WelcomePageFeedbackDTO> GetForFrontPage();
        bool ApproveFeedback(int id);
        bool DenyFeedback(int id);
        bool MakePending(int id);
    }
}
