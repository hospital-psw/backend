namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.Tender.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.Util.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TenderBloodTransferStatisticsService : IUrgentBloodTransferStatisticsService
    {
        private readonly ITenderService _tenderService;
        private readonly IHTMLReportService _htmlReportService;

        public TenderBloodTransferStatisticsService(ITenderService tenderService, IHTMLReportService htmlReportService)
        {
            _tenderService = tenderService;
            _htmlReportService = htmlReportService;
        }

        public string GenerateHTMLReport(DateTime from, DateTime to)
        {
            Dictionary<string, double> bbShare = GetBloodBankShare(from, to);
            Dictionary<string, double> btShare = GetBloodTypeShare(from, to);
            Dictionary<string, double> btAmount = GetAmountByBloodUnit(from, to);
            Dictionary<string, Dictionary<string, double>> bloodBanks = GetAmountByBloodBankByBloodGroup(from, to);
            List<List<string>> convertedBloodBanks = ConvertNestedDictionaryToLists(bloodBanks);
            _htmlReportService.AddTable(new List<string>(new string[] { "Blood bank", "Blood type", "Amount" }), convertedBloodBanks);
            _htmlReportService.AddPieChart(new List<string>(bbShare.Keys), new List<double>(bbShare.Values));
            _htmlReportService.AddPieChart(new List<string>(btShare.Keys), new List<double>(btShare.Values));
            _htmlReportService.AddBarChart(new List<string>(btAmount.Keys), new List<double>(btAmount.Values));
            return _htmlReportService.OutputFile;
        }

        private List<List<string>> ConvertNestedDictionaryToLists(Dictionary<string, Dictionary<string, double>> input)
        {
            List<List<string>> data = new List<List<string>>();
            foreach (var bloodBank in input)
            {
                List<string> row = new List<string>();
                foreach (var bloodType in bloodBank.Value)
                {
                    row.Add(bloodBank.Key);
                    row.Add(bloodType.Key);
                    row.Add(bloodType.Value.ToString());
                    data.Add(row);
                    row = new List<string>();
                }
            }
            return data;
        }

        private List<Tender> RequestsInRange(DateTime from, DateTime to)
        {
            return _tenderService.GetAll().Where(x => from <= x.DateCreated && x.DateCreated <= to).ToList();
        }

        private Dictionary<string, double> NormalizeDictionary(Dictionary<string, double> dictionary)
        {
            double valueSum = 0;
            foreach (var element in dictionary)
            {
                valueSum += element.Value;
            }
            Dictionary<string, double> result = new Dictionary<string, double>();
            foreach (var element in dictionary)
            {
                result[element.Key] = element.Value / valueSum;
            }
            return result;
        }

        public Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to)
        {
            Dictionary<string, Dictionary<string, double>> bloodBanks = new Dictionary<string, Dictionary<string, double>>();
            foreach (Tender request in RequestsInRange(from, to))
            {
                foreach(TenderItem tenderItem in request.Items) { 
                    if (bloodBanks.ContainsKey(request.Sender.Name))
                    {
                        var bloodBank = bloodBanks[request.Sender.Name];
                        if (bloodBank.ContainsKey(tenderItem.BloodType.ToString()))
                        {
                            bloodBank[tenderItem.BloodType.ToString()] += tenderItem.Quantity;
                        }
                        else
                        {
                            bloodBank.Add(tenderItem.BloodType.ToString(), tenderItem.Quantity);
                        }
                    }
                    else
                    {
                        Dictionary<string, double> newBloodBankBloodType = new Dictionary<string, double>();
                        newBloodBankBloodType.Add(tenderItem.BloodType.ToString(), tenderItem.Quantity);
                        bloodBanks.Add(request.Sender.Name, newBloodBankBloodType);
                    }
                }
            }
            return bloodBanks;
        }

        public Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodTypes = new Dictionary<string, double>();
            foreach (Tender request in RequestsInRange(from, to))
            {
                foreach (TenderItem tenderItem in request.Items)
                {

                    if (bloodTypes.ContainsKey(tenderItem.BloodType.ToString()))
                    {
                        bloodTypes[tenderItem.BloodType.ToString()] += tenderItem.Quantity;
                    }
                    else
                    {
                        bloodTypes.Add(tenderItem.BloodType.ToString(), tenderItem.Quantity);
                    }
                }
            }
            return bloodTypes;
        }

        public Dictionary<string, double> GetBloodBankShare(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodBanks = new Dictionary<string, double>();
            foreach (Tender request in RequestsInRange(from, to))
            {
                foreach (TenderItem tenderItem in request.Items)
                {
                    if (bloodBanks.ContainsKey(request.Sender.Name))
                    {
                        bloodBanks[request.Sender.Name] += tenderItem.Quantity;
                    }
                    else
                    {
                        bloodBanks.Add(request.Sender.Name, tenderItem.Quantity);
                    }
                }
            }
            return NormalizeDictionary(bloodBanks);
        }

        public Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodTypes = new Dictionary<string, double>();
            foreach (Tender request in RequestsInRange(from, to))
            {
                foreach (TenderItem tenderItem in request.Items) {

                    if (bloodTypes.ContainsKey(tenderItem.BloodType.ToString()))
                    {
                        bloodTypes[tenderItem.BloodType.ToString()] += tenderItem.Quantity;
                    }
                    else
                    {
                        bloodTypes.Add(tenderItem.BloodType.ToString(), tenderItem.Quantity);
                    }
                }
            }
            return NormalizeDictionary(bloodTypes);
        }
    }
}
