namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Tender.Enums;
    using IntegrationLibrary.Tender.Interfaces;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Logging;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Xml.Linq;

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
                List<TenderOffer> Offers = new List<TenderOffer>();
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

        public void FinishTender(int tenderId, int offerIndex)
        {
            try
            {
                var entity = _unitOfWork.TenderRepository.Get(tenderId);
                entity.Status = TenderStatus.CLOSED;
                entity.TenderWinner = entity.Offers[offerIndex];

                string templateWin = MailSender.MakeWinningTemplate(tenderId);
                string templateLose = MailSender.MakeLoseTemplate(tenderId);
                _mailer.SendEmail(templateWin, "You won a tender", entity.Offers[offerIndex].Offeror.Email);

                int i = 0;
                foreach (var offer in entity.Offers)
                {
                    if (i != offerIndex)
                    {
                        _mailer.SendEmail(templateLose, "Tender finished", offer.Offeror.Email);
                    }
                    i++;
                }
                _unitOfWork.TenderRepository.Update(entity);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in Delete {e.Message} in {e.StackTrace}");
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

        public List<Tender> GetActive()
        {
            try
            {
                return _unitOfWork.TenderRepository.GetAll().Where(x => x.Status == TenderStatus.OPEN && (x.DueDate > DateTime.Now || x.DueDate == DateTime.MinValue)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in TenderService in GetActive {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public List<double> GetMoneyPerMonth(int year)
        {
            List<double> moneyPerMonth = new List<double>();
            var allMonths = from month in Enumerable.Range(1, 12)
                            let key = new { Month = month }
                            join tender in _unitOfWork.TenderRepository.GetAll().Where(t => t.TenderWinner != null && t.TenderWinner.DateCreated.Year == year) on key
                            equals new { tender.TenderWinner.DateCreated.Month } into g
                            select new { key, total = g.Sum(tender => tender.TenderWinner.Items.Sum(item => item.Money.Amount)) };
            foreach (var element in allMonths)
            {
                moneyPerMonth.Add(element.total);
            }
            return moneyPerMonth;
        }

        public List<double> GetBloodPerMonth(int year, int bloodType)
        {
            BloodType bt = convertIntToBloodType(bloodType);
            List<double> bloodPerMonth = new List<double>();
            for (int i = 0; i < 12; ++i)
            {
                bloodPerMonth.Add(0);
            }
            foreach (Tender tender in _unitOfWork.TenderRepository.GetAll())
            {
                if (tender.TenderWinner != null && tender.DueDate.Year == year)
                {
                    bloodPerMonth = CalculateBloodPerMonth(tender, bloodPerMonth, bt);
                }
            }
            return bloodPerMonth;
        }

        private BloodType convertIntToBloodType(int bloodType)
        {
            BloodType bt;
            switch (bloodType)
            {
                case 0:
                    bt = BloodType.A_POSITIVE;
                    break;
                case 1:
                    bt = BloodType.A_NEGATIVE;
                    break;
                case 2:
                    bt = BloodType.B_POSITIVE;
                    break;
                case 3:
                    bt = BloodType.B_NEGATIVE;
                    break;
                case 4:
                    bt = BloodType.O_POSITIVE;
                    break;
                case 5:
                    bt = BloodType.O_NEGATIVE;
                    break;
                case 6:
                    bt = BloodType.AB_POSITIVE;
                    break;
                case 7:
                    bt = BloodType.AB_NEGATIVE;
                    break;
                default:
                    bt = BloodType.A_POSITIVE;
                    break;

            }
            return bt;
        }

        private List<double> CalculateBloodPerMonth(Tender tender, List<double> bloodPerMonth, BloodType bt)
        {
            foreach (TenderItem tenderItem in tender.TenderWinner.Items)
            {
                if (tenderItem.BloodType.Equals(bt))
                {
                    bloodPerMonth[tender.DueDate.Month - 1] += tenderItem.Quantity;
                }
            }
            return bloodPerMonth;
        }
    }
}
