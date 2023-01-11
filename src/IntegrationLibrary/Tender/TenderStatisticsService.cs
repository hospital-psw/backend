namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.Tender.Interfaces;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TenderStatisticsService : ITenderStatisticsService
    {
        private readonly ITenderService _tenderService;
        private readonly IHTMLReportService _htmlReportService;
        private readonly ISFTPService _sftpService;
        private readonly IMailSender _mailSender;

        public TenderStatisticsService(ITenderService tenderService, IHTMLReportService htmlReportService, ISFTPService sftpService, IMailSender mailSender)
        {
            _tenderService = tenderService;
            _htmlReportService = htmlReportService;
            _sftpService = sftpService;
            _mailSender = mailSender;
        }
        public Stream GenerateHTMLReport(DateTime from, DateTime to)
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
            string template = MailSender.MakeUrgentBloodRequestTemplate();
            _mailSender.SendEmail(template, "Tender transfer report", "psw.hospital.2022@gmail.com", reportFile);
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
        private List<Tender> RequestsInRange(DateTime from, DateTime to)
        {
            return _tenderService.GetAll().Where(x => x.Status == Enums.TenderStatus.CLOSED && x.TenderWinner!=null && from <= x.DateUpdated && x.DateUpdated <= to).ToList();
        }
        public Dictionary<string, Dictionary<string, double>> GetAmountByBloodBankByBloodGroup(DateTime from, DateTime to)
        {
            Dictionary<string, Dictionary<string, double>> bloodBanks = new Dictionary<string, Dictionary<string, double>>();
            foreach (Tender request in RequestsInRange(from, to))
            {
                foreach (TenderItem item in request.Items) { 
                     if (bloodBanks.ContainsKey(request.TenderWinner.Offeror.Name))
                         {
                            var bloodBank = bloodBanks[request.TenderWinner.Offeror.Name];
                            if (bloodBank.ContainsKey(item.BloodType.ToString()))
                            {
                                bloodBank[item.BloodType.ToString()] += item.Quantity;
                            }
                            else
                            {
                                bloodBank.Add(item.BloodType.ToString(), item.Quantity);
                            }
                        }
                        else
                        {
                            Dictionary<string, double> newBloodBankBloodType = new Dictionary<string, double>();
                            newBloodBankBloodType.Add(item.BloodType.ToString(), item.Quantity);
                            bloodBanks.Add(request.TenderWinner.Offeror.Name, newBloodBankBloodType);
                        }
                }
            }
            return bloodBanks;
        }

        public Dictionary<string, double> GetAmountByBloodUnit(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodTypes = new Dictionary<string, double>();
            foreach (Tender tender in RequestsInRange(from, to))
            {
                foreach (TenderItem item in tender.Items)
                {
                    if (bloodTypes.ContainsKey(item.BloodType.ToString()))
                    {
                        bloodTypes[item.BloodType.ToString()] += item.Quantity;
                    }
                    else
                    {
                        bloodTypes.Add(item.BloodType.ToString(), item.Quantity);
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
                if (bloodBanks.ContainsKey(request.TenderWinner.Offeror.Name))
                {
                    bloodBanks[request.TenderWinner.Offeror.Name] += request.TenderWinner.TotalSum().Amount;
                }
                else
                {
                    bloodBanks.Add(request.TenderWinner.Offeror.Name, request.TenderWinner.TotalSum().Amount);
                }
            }
            return bloodBanks;
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
        public Dictionary<string, double> GetBloodTypeShare(DateTime from, DateTime to)
        {
            Dictionary<string, double> bloodTypes = new Dictionary<string, double>();
            foreach (Tender tender in RequestsInRange(from, to))
            {
                foreach(TenderItem item in tender.Items)
                {
                    if (bloodTypes.ContainsKey(item.BloodType.ToString()))
                    {
                        bloodTypes[item.BloodType.ToString()] += item.Quantity;
                    }
                    else
                    {
                        bloodTypes.Add(item.BloodType.ToString(), item.Quantity);
                    }
                }
                
            }
            return NormalizeDictionary(bloodTypes);
        }
    }
}
