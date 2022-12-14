namespace HospitalLibrary.Util
{
    using HospitalLibrary.Core.DTO.PDF;
    using HospitalLibrary.Core.Model.Examinations;
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

        public static void GenerateAnamnesisPDF(Anamnesis anamnesis, AnamnesisPdfDTO dto) {

            var Renderer = new IronPdf.ChromePdfRenderer();
            string text = System.IO.File.ReadAllText(@"./../HospitalLibrary/Resources/PDF/AnamnesisPdfTemplate.html");
            text = text.Replace("START_DATE", anamnesis.Appointment.Date.ToString("dd.MM.yyyy."));
            text = text.Replace("SURNAME", anamnesis.Appointment.Patient.LastName);
            text = text.Replace("NAME", anamnesis.Appointment.Patient.FirstName);
            text = text.Replace("BLOOD_TYPE", EnumStringConverters.GetString(anamnesis.Appointment.Patient.BloodType));

            if (dto.IsDescriptionSelected)
            {
                text = text.Replace("ANAMNESIS_DESCRIPTION", anamnesis.Description);
            }
            else {
                text = text.Replace("ANAMNESIS_DESCRIPTION", "/");
            }
            if (dto.AreSymptomsSelected) {
                text = text.Replace("SYMPTOMS", GenerateSymptomsList(anamnesis));
            }
            else
            {
                text = text.Replace("SYMPTOMS", "/");
            }
            if (dto.AreRecepiesSelected)
            {
                text = text.Replace("PRESCRIPTIONS_LIST", GeneratePrescriptionsList(anamnesis));
            }
            else
            {
                text = text.Replace("PRESCRIPTIONS_LIST", "/");
            }


            PdfDocument PDF = Renderer.RenderHtmlAsPdf(text);
            PDF.SaveAs("./../HospitalLibrary/Resources/PDF/anamnesis.pdf");
        }


        private static string GeneratePrescriptionsList(Anamnesis anamnesis)
        {
            string txt = "";

            foreach (Prescription p in anamnesis.Prescriptions)
            {
                string helper =
                       "<div class='terapija'>" +
                       "<div class='naziv-leka'>" +
                       "<div class='ime'>" + p.Medicament.Name + "</div>" +
                       "<div class='kolicina'>" + p.Medicament.Quantity + " units</div>" +
                       "</div>" +
                       "<div class='datum-leka'>From:" + p.DateRange.From.ToString("dd.MM.yyyy.") + " | To: " + p.DateRange.To.ToString("dd.MM.yyyy.") + "</div>" +
                       "</div>";

                txt += helper;
            }

            return txt;
        }

        private static string GenerateSymptomsList(Anamnesis anamnesis)
        {
            string txt = "";

            foreach (Symptom s in anamnesis.Symptoms)
            {
                string helper =
                       "<div class='terapija'>" +
                       "<div class='naziv-leka'>" +
                       "<div class='ime'>" + s.Name + "</div>" +
                       "</div>" +
                       "</div>";

                txt += helper;
            }

            return txt;
        }

    }
}
