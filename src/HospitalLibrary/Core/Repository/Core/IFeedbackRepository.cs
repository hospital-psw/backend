namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFeedbackRepository : IBaseRepository<Feedback>
    {
        IEnumerable<Feedback> GetAllPublic();
        IEnumerable<Feedback> GetAllPrivate();
        IEnumerable<Feedback> GetAllAnonymous();
        IEnumerable<Feedback> GetAllIdentified();

        IEnumerable<Feedback> GetAllApproved();


        IEnumerable<Feedback> GetAllDenied();
        IEnumerable<Feedback> GetAllPending();

    }
}
