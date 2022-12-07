namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Tender.Interfaces;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public class TenderService : ITenderService
    {

        private readonly ILogger<Tender> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailSender _mailer;
        public TenderService(ILogger<Tender> logger, IUnitOfWork unitOfWork, IMailSender mailer)
        {
            _logger = logger;
            _mailer = mailer;
            _unitOfWork = unitOfWork;
        }

        public Tender Create(Tender entity)
        {
            try
            {
                _unitOfWork.TenderRepository.Add(entity);
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
                var entity = _unitOfWork.TenderRepository.Get(id);
                entity.Deleted = true;
                _unitOfWork.TenderRepository.Update(entity);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in Delete {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public Tender Get(int id)
        {
            try
            {
                return _unitOfWork.TenderRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Tender> GetAll()
        {
            try
            {
                return _unitOfWork.TenderRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public TenderOffer MakeAnOffer(int tenderId, TenderOffer offer)
        {
            try
            {
                Tender tender = Get(tenderId);
                if (tender == null)
                {
                    return null;
                }
                TenderOffer acceptedOffer = tender.MakeAnOffer(offer);
                if (acceptedOffer == null)
                {
                    return null;
                }
                offer.Offeror = _unitOfWork.BloodBankRepository.Get(offer.Offeror.Id);
                _unitOfWork.TenderRepository.Update(tender);
                _unitOfWork.Save();
                return acceptedOffer;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in MakeAnOffer {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Tender Update(Tender entity)
        {
            try
            {
                _unitOfWork.TenderRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in Update {e.Message} in {e.StackTrace}");
                return null;
            }
        }
        public double AvgTotalPrice()
        {
            throw new NotImplementedException();
        }

        public double WinningOfferPrice()
        {
            throw new NotImplementedException();
        }

        public Tender GetActive()
        {
            throw new NotImplementedException();
        }
    }
}
