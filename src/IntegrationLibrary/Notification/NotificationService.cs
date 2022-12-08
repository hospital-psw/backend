namespace IntegrationLibrary.Notification
{
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Notification.Interfaces;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Logging;
    using OpenQA.Selenium.DevTools.V105.HeapProfiler;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotificationService : INotificationService
    {
        private readonly ILogger<Notification> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailSender _mailer;
        private readonly IBBConnections _connections;

        public NotificationService(ILogger<Notification> logger, IUnitOfWork unitOfWork, IMailSender mailer, IBBConnections connections)
        {
            _logger = logger;
            _mailer = mailer;
            _connections = connections;
            _unitOfWork = unitOfWork;
        }

        public Notification Create(Notification entity)
        {
            try
            {
                _unitOfWork.NotificationRepository.Add(entity);
                _unitOfWork.Save();

                return entity;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in Create {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _unitOfWork.NotificationRepository.Get(id);
                entity.Deleted = true;
                _unitOfWork.NotificationRepository.Update(entity);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NotificationService in Delete {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public Notification Get(int id)
        {
            try
            {
                return _unitOfWork.NotificationRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NotificationService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Notification> GetAll()
        {
            try
            {
                return _unitOfWork.NotificationRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NotificationService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Notification Update(Notification entity)
        {
            try
            {
                _unitOfWork.NotificationRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in NotificationService in Update {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
