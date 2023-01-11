namespace IntegrationLibrary.UrgentBloodTransfer
{
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class UrgentBloodTransferStatisticsService : IUrgentBloodTransferStatisticsService
    {
        private readonly IUrgentBloodTransferService _urgentBloodTransferService;
        private readonly IHTMLReportService _htmlReportService;
        private readonly ISFTPService _sftpService;
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;

        public UrgentBloodTransferStatisticsService(IUrgentBloodTransferService urgentBloodTransferService, IHTMLReportService htmlReportService, ISFTPService sftpService, IMailSender mailSender, IConfiguration configuration)
        {
            _urgentBloodTransferService = urgentBloodTransferService;
            _htmlReportService = htmlReportService;
            _sftpService = sftpService;
            _mailSender = mailSender;
            _configuration = configuration;
        }

        public Stream GenerateHTMLReport(DateTime from, DateTime to, bool sendEmail)
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
            _htmlReportService.AddTimestamp(from, to);

            var reportFile = PdfSharpConvert(_htmlReportService.OutputFile);
            _sftpService.SendFile(reportFile);
            if (sendEmail)
            {
                string template = MailSender.MakeUrgentBloodRequestTemplate();
                _mailSender.SendEmail(template, "Urgent blood transfer report", _configuration["ManagerEmail"], reportFile);
            }
            return reportFile;
        }


        private static Stream PdfSharpConvert(String html)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            Renderer.RenderingOptions.MarginLeft = 0;
            Renderer.RenderingOptions.MarginRight = 0;
            Renderer.RenderingOptions.MarginTop = 0;
            Renderer.RenderingOptions.MarginBottom = 0;
            Renderer.RenderingOptions.EnableJavaScript = true;
            var pdf = Renderer.RenderHtmlAsPdf(html);
            return pdf.Stream;
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

        private List<UrgentBloodTransfer> RequestsInRange(DateTime from, DateTime to)
        {
            return _urgentBloodTransferService.GetAll().Where(x => from <= x.DateCreated && x.DateCreated <= to && x.Sender != null).ToList();
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
            foreach (UrgentBloodTransfer request in RequestsInRange(from, to))
            {
                if (bloodBanks.ContainsKey(request.Sender.Name))
                {
                    var bloodBank = bloodBanks[request.Sender.Name];
                    if (bloodBank.ContainsKey(request.BloodType.ToString()))
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
