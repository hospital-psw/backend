namespace IntegrationServices.ReportService
{
    using IntegrationServices.ReportService.DTO;
    using IronPdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GeneratePDF
    {
        public static string GeneratePdf(string bbName, CalculateDTO dto)
        {
            string text = System.IO.File.ReadAllText(@"template.html");
            text = text.Replace("BLOOD_BANK_NAME", bbName);
            text = text.Replace("A_PLUS", dto.APlusAmount.ToString());
            text = text.Replace("A_MINUS", dto.AMinusAmount.ToString());
            text = text.Replace("B_PLUS", dto.BPlusAmount.ToString());
            text = text.Replace("B_MINUS", dto.BMinusAmount.ToString());
            text = text.Replace("Z_PLUS", dto.ABPlusAmount.ToString());
            text = text.Replace("Z_MINUS", dto.ABMinusAmount.ToString());
            text = text.Replace("O_PLUS", dto.OPlusAmount.ToString());
            text = text.Replace("O_MINUS", dto.OMinusAmount.ToString());
            text = text.Replace("TOTAL_TOT", dto.TotalSum.ToString());

            var Renderer = new ChromePdfRenderer();
            var PDF = Renderer.RenderHtmlAsPdf(text);
            Console.WriteLine("KUC-KUC");
            string genName = DateTime.Now.ToString().Split(' ')[0].Replace('/', '-') + "-report.pdf";
            PDF.SaveAs(@"./../PDF/" + genName);
            return genName;
        }
    }
}
