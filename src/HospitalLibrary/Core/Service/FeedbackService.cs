﻿namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        private readonly ILogger<Feedback> _logger;

        public FeedbackService(ILogger<Feedback> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public Feedback Add(NewFeedbackDTO dto)
        {
            try
            {
                Feedback feedback = new Feedback(dto);
                feedback.Creator = (ApplicationPatient)_unitOfWork.ApplicationUserRepository.Get(dto.CreatorId);
                _unitOfWork.FeedbackRepository.Add(feedback);
                _unitOfWork.Save();

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
                return _unitOfWork.FeedbackRepository.GetAll();
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
                return _unitOfWork.FeedbackRepository.Get(id);
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
                return _unitOfWork.FeedbackRepository.GetAllPublic();
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
                return _unitOfWork.FeedbackRepository.GetAllPrivate();
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
                return _unitOfWork.FeedbackRepository.GetAllAnonymous();
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
                return _unitOfWork.FeedbackRepository.GetAllIdentified();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in GetAllIdentifiedFeedback {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Feedback> GetAllAproved()
        {
            try
            {
                return _unitOfWork.FeedbackRepository.GetAllApproved();
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
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Public = true;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
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
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Public = false;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
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
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Anonymous = true;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
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
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Anonymous = false;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
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
                foreach (Feedback feedback in _unitOfWork.FeedbackRepository.GetAllApproved())
                {
                    if (feedback.Anonymous)
                    {
                        WelcomePageFeedbackDTO dto = new WelcomePageFeedbackDTO("Anonymous", feedback.Message.Message);
                        feedbacks.Add(dto);
                    }
                    else
                    {
                        WelcomePageFeedbackDTO dto = new WelcomePageFeedbackDTO(feedback.Creator.FirstName + " " + feedback.Creator.LastName, feedback.Message.Message);
                        feedbacks.Add(dto);
                    }
                }
                return feedbacks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ApproveFeedback(int id)
        {
            try
            {
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Status = FeedbackStatus.APPROVED;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in ApproveFeedback {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public bool DenyFeedback(int id)
        {
            try
            {
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Status = FeedbackStatus.DENIED;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in DenyFeedback {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public bool MakePending(int id)
        {
            try
            {
                Feedback feedback = _unitOfWork.FeedbackRepository.Get(id);

                if (feedback == null)
                {
                    return false;

                }

                feedback.Status = FeedbackStatus.PENDING;
                _unitOfWork.FeedbackRepository.Update(feedback);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in MakePending {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public IEnumerable<Feedback> GetAllDeniedFeedback()
        {
            try
            {
                return _unitOfWork.FeedbackRepository.GetAllDenied();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in GetAllDeniedFeedback {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Feedback> GetAllPendingFeedback()
        {
            try
            {
                return _unitOfWork.FeedbackRepository.GetAllPending();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in FeedbackService in GetAllPendingFeedback {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
