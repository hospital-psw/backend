namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.DTO.FeedBack;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        private readonly ILogger<Feedback> _logger;

        public FeedbackService(ILogger<Feedback> logger) : base()
        {
            _logger = logger;
        }

        public Feedback Add(NewFeedbackDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Feedback feedback = new Feedback(dto);
                feedback.Creator = unitOfWork.UserRepository.Get(dto.CreatorId);
                unitOfWork.FeedbackRepository.Add(feedback);
                unitOfWork.Save();

                return feedback;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in Add {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<Feedback> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.FeedbackRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Feedback Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.FeedbackRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
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
                _logger.LogError($"Error in FeedbackService in GetAllPublicFeedback {e.Message} in {e.StackTrace}");
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
                _logger.LogError($"Error in FeedbackService in GetAllPrivateFeedback {e.Message} in {e.StackTrace}");
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
                _logger.LogError($"Error in FeedbackService in GetAllAnonymousFeedback {e.Message} in {e.StackTrace}");
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
                _logger.LogError($"Error in FeedbackService in GetAllIdentifiedFeedback {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public bool MakePublic(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Feedback feedback = unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Public = true;
                unitOfWork.FeedbackRepository.Update(feedback);
                unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in MakePublic {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public bool MakePrivate(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Feedback feedback = unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Public = false;
                unitOfWork.FeedbackRepository.Update(feedback);
                unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in MakePrivate {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public bool MakeAnonymous(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Feedback feedback = unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Anonymous = true;
                unitOfWork.FeedbackRepository.Update(feedback);
                unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in MakeAnonymous {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public bool MakeIdentified(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Feedback feedback = unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Anonymous = false;
                unitOfWork.FeedbackRepository.Update(feedback);
                unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in MakeIdentified {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public List<WelcomePageFeedbackDTO> GetForFrontPage()
        {
            try
            {
                List<WelcomePageFeedbackDTO> feedbacks = new List<WelcomePageFeedbackDTO>();
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                foreach (Feedback feedback in unitOfWork.FeedbackRepository.GetAllPublic())
                {

                    if (feedback.Anonymous)
                    {
                        WelcomePageFeedbackDTO dto = new WelcomePageFeedbackDTO("Anonymous", feedback.Message);
                        feedbacks.Add(dto);
                    }
                    else
                    {
                        WelcomePageFeedbackDTO dto = new WelcomePageFeedbackDTO(feedback.Creator.FirstName + " " + feedback.Creator.LastName, feedback.Message);
                        feedbacks.Add(dto);
                    }
                }
                return feedbacks;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
