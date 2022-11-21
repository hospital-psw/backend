namespace HospitalLibrary.Util
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using IronPdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PDFUtil
    {
        public static void GenerateTreatmentPdf(MedicalTreatment treatment)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            string text = System.IO.File.ReadAllText(@"./../HospitalLibrary/Resources/PDF/PdfTemplate.html");
            text = text.Replace("START_DATE", treatment.Start.ToString("dd.MM.yyyy."));
            text = text.Replace("END_DATE", treatment.End.ToString("dd.MM.yyyy."));
            text = text.Replace("SURNAME", treatment.Patient.LastName);
            text = text.Replace("NAME", treatment.Patient.FirstName);
            text = text.Replace("BLOOD_TYPE", EnumStringConverters.GetString(treatment.Patient.BloodType));
            text = text.Replace("ADMITTANCE_REASON", treatment.AdmittanceReason);
            text = text.Replace("REPORT_Z", treatment.Report);
            text = text.Replace("THERAPIES_LIST", GenerateTherapiesList(treatment));

            PdfDocument PDF = Renderer.RenderHtmlAsPdf(text);
            PDF.SaveAs("./../HospitalLibrary/Resources/PDF/treatment.pdf");
        }

        private static string GenerateTherapiesList(MedicalTreatment treatment)
        {
            string txt = "";

            foreach (BloodUnitTherapy th in treatment.BloodUnitTherapies)
            {
                string helper =
                       "<div class='terapija'>" +
                       "<div class='naziv-leka'>" +
                       "<div class='ime'>" + EnumStringConverters.GetString(th.BloodUnit.BloodType) + "</div>" +
                       "<div class='kolicina'>" + th.AmountOfBloodUnit + " units</div>" +
                       "</div>" +
                       "<div class='datum-leka'>From:" + th.Start.ToString("dd.MM.yyyy.") + " | To: " + th.Start.ToString("dd.MM.yyyy.") + "</div>" +
                       "</div>";

                txt += helper;
            }

            foreach (MedicamentTherapy th in treatment.MedicamentTherapies)
            {
                string helper =
                       "<div class='terapija'>" +
                       "<div class='naziv-leka'>" +
                       "<div class='ime'>" + th.Medicament.Name + "</div>" +
                       "<div class='kolicina'>" + th.AmountOfMedicament + " units</div>" +
                       "</div>" +
                       "<div class='datum-leka'>From:" + th.Start.ToString("dd.MM.yyyy.") + " | To: " + th.Start.ToString("dd.MM.yyyy.") + "</div>" +
                       "</div>";

                txt += helper;
            }

            return txt;
        }
    }
}
