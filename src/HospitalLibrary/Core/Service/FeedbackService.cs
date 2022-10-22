namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        public FeedbackService() : base() 
        {
        }

        public IEnumerable<Feedback> GetAllPublicFeedback()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.FeedbackRepository.GetAllPublic();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Feedback> GetAllPrivateFeedback()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.FeedbackRepository.GetAllPrivate();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Feedback> GetAllAnonymousFeedback()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.FeedbackRepository.GetAllAnonymous();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Feedback> GetAllIdentifiedFeedback()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.FeedbackRepository.GetAllIdentified();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool MakeFeedbackPublic(int id) 
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Feedback feedback = unitOfWork.FeedbackRepository.Get(id);
                feedback.Public = true;
                unitOfWork.FeedbackRepository.Update(feedback);
                unitOfWork.Save();
                return true;
            }
            catch (Exception e) 
            {
                return false;
            }
        }
    }
}
