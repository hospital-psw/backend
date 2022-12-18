namespace IntegrationLibrary.UrgentBloodTransfer
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BloodBank;

    public class UrgentBloodTransferStatisticsService : IUrgentBloodTransferStatisticsService
    {
        private IUrgentBloodTransferService _urgentBloodTransferService;

        public UrgentBloodTransferStatisticsService(IUrgentBloodTransferService urgentBloodTransferService)
        {
            _urgentBloodTransferService = urgentBloodTransferService;
        }

        private List<UrgentBloodTransfer> RequestsInRange(DateTime from, DateTime to)
        {
            return _urgentBloodTransferService.GetAll().Where(x =>  from <= x.DateCreated && x.DateCreated <= to).ToList();
        }

        private Dictionary<string, double> NormalizeDictionary(Dictionary<string, double> dictionary)
        {
            double valueSum = 0;
            foreach(var element in dictionary)
            {
                valueSum += element.Value;
            }
            Dictionary<string, double> result = new Dictionary<string, double>();
            foreach(var element in dictionary)
            {
                result[element.Key] = element.Value / valueSum;
            }
            return result;
        }
        public Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to)
        {
            Dictionary<string, Dictionary<string, double>> bloodBanks = new Dictionary<string, Dictionary<string, double>>();
            foreach(UrgentBloodTransfer request in RequestsInRange(from, to))
            {
                if (bloodBanks.ContainsKey(request.Sender.Name))
                {
                    var bloodBank = bloodBanks[request.Sender.Name];
                    if(bloodBank.ContainsKey(request.BloodType.ToString()))
                    {
                        bloodBank[request.BloodType.ToString()] += request.Amount;
                    }
                    else
                    {
                        bloodBank.Add(request.BloodType.ToString(), request.Amount);
                    }
                }
                else
                {
                    Dictionary<string, double> newBloodBankBloodType = new Dictionary<string, double>();
                    newBloodBankBloodType.Add(request.BloodType.ToString(), request.Amount);
                    bloodBanks.Add(request.Sender.Name, newBloodBankBloodType);
                }
            }
            return bloodBanks;
        }

        public Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodTypes = new Dictionary<string, double>();
            foreach(UrgentBloodTransfer request in RequestsInRange(from, to))
            {
                if(bloodTypes.ContainsKey(request.BloodType.ToString()))
                {
                    bloodTypes[request.BloodType.ToString()] += request.Amount;
                }
                else
                {
                    bloodTypes.Add(request.BloodType.ToString(), request.Amount);
                }
            }
            return bloodTypes;
        }

        public Dictionary<string, double> GetBloodBankShare(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodBanks = new Dictionary<string, double>();
            foreach (UrgentBloodTransfer request in RequestsInRange(from, to))
            {
                if (bloodBanks.ContainsKey(request.Sender.Name))
                {
                    bloodBanks[request.Sender.Name] += request.Amount;
                }
                else
                {
                    bloodBanks.Add(request.Sender.Name, request.Amount);
                }
            }
            return NormalizeDictionary(bloodBanks);
        }

        public Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodTypes = new Dictionary<string, double>();
            foreach (UrgentBloodTransfer request in RequestsInRange(from, to))
            {
                if (bloodTypes.ContainsKey(request.BloodType.ToString()))
                {
                    bloodTypes[request.BloodType.ToString()] += request.Amount;
                }
                else
                {
                    bloodTypes.Add(request.BloodType.ToString(), request.Amount);
                }
            }
            return NormalizeDictionary(bloodTypes);
        }
    }
}
