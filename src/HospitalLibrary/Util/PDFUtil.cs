namespace HospitalLibrary.Util
{
    using HospitalLibrary.Core.DTO.PDF;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using IronPdf;
    using System.IO;

    public class PDFUtil : IPDFUtil
    {
        public PDFUtil()
        {

        }
        public Stream GenerateTreatmentPdf(MedicalTreatment treatment)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            //string text = System.IO.File.ReadAllText(@"./../HospitalLibrary/Resources/PDF/PdfTemplate.html");
            string text = PdfTemplate();
            text = text.Replace("START_DATE", treatment.Start.ToString("dd.MM.yyyy."));
            text = text.Replace("END_DATE", treatment.End.ToString("dd.MM.yyyy."));
            text = text.Replace("SURNAME", treatment.Patient.LastName);
            text = text.Replace("NAME", treatment.Patient.FirstName);
            text = text.Replace("BLOOD_TYPE", EnumStringConverters.GetString(treatment.Patient.BloodType));
            text = text.Replace("ADMITTANCE_REASON", treatment.AdmittanceReason);
            text = text.Replace("REPORT_Z", treatment.Report);
            text = text.Replace("THERAPIES_LIST", GenerateTherapiesList(treatment));

            PdfDocument PDF = Renderer.RenderHtmlAsPdf(text);
            //PDF.SaveAs("./../HospitalLibrary/Resources/PDF/treatment.pdf");
            return PDF.Stream;
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

        public Stream GenerateAnamnesisPDF(Anamnesis anamnesis, AnamnesisPdfDTO dto)
        {

            var Renderer = new IronPdf.ChromePdfRenderer();
            //string text = System.IO.File.ReadAllText(@"./../HospitalLibrary/Resources/PDF/AnamnesisPdfTemplate.html");
            string text = AnamnesisPdfTemplate();
            text = text.Replace("START_DATE", anamnesis.Appointment.Date.ToString("dd.MM.yyyy."));
            text = text.Replace("SURNAME", anamnesis.Appointment.Patient.LastName);
            text = text.Replace("NAME", anamnesis.Appointment.Patient.FirstName);
            text = text.Replace("BLOOD_TYPE", EnumStringConverters.GetString(anamnesis.Appointment.Patient.BloodType));

            if (dto.IsDescriptionSelected)
            {
                text = text.Replace("ANAMNESIS_DESCRIPTION", anamnesis.Description);
            }
            else
            {
                text = text.Replace("ANAMNESIS_DESCRIPTION", "/");
            }
            if (dto.AreSymptomsSelected)
            {
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
            //PDF.SaveAs("./../HospitalLibrary/Resources/PDF/anamnesis.pdf");
            return PDF.Stream;
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

        private static string AnamnesisPdfTemplate()
        {
            return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n    <style>\r\n        html {\r\n            font-size: 10px;\r\n        }\r\n\r\n        body {\r\n        }\r\n\r\n        .header {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n            margin-top: 0rem;\r\n        }\r\n\r\n        .logo-container {\r\n            /*background-image: url('./logo.png');\r\n            background-repeat: no-repeat;\r\n            background-position: center;\r\n            background-size: cover;*/\r\n            height: 10rem;\r\n            width: 10rem;\r\n            padding: 0rem;\r\n        }\r\n\r\n        .log {\r\n            height: 10rem;\r\n            width: 10rem;\r\n        }\r\n\r\n        .address {\r\n            font-size: 1.3rem;\r\n            line-height: 1rem;\r\n        }\r\n\r\n            .address p {\r\n                display: flex;\r\n                justify-content: flex-end;\r\n            }\r\n\r\n        .main-content {\r\n            display: flex;\r\n            justify-content: center;\r\n            flex-direction: column;\r\n        }\r\n\r\n        .header-text {\r\n            margin-bottom: 2.5rem;\r\n        }\r\n\r\n        .head {\r\n            font-size: 3rem;\r\n            display: flex;\r\n            justify-content: center;\r\n        }\r\n\r\n        .date {\r\n            font-size: 2.3rem;\r\n            display: flex;\r\n            justify-content: center;\r\n            font-weight: 500;\r\n        }\r\n\r\n        .patient-info {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            padding: 0rem 1rem;\r\n            margin-bottom: 3rem;\r\n        }\r\n\r\n        .col {\r\n            display: flex;\r\n            flex-direction: column;\r\n            justify-content: flex-start;\r\n            width: 45%;\r\n        }\r\n\r\n        .stavka {\r\n            margin-bottom: 2rem;\r\n            display: flex;\r\n            justify-content: space-between;\r\n            font-size: 1.5rem;\r\n        }\r\n\r\n        .label {\r\n            width: 40%;\r\n            font-weight: 600;\r\n        }\r\n\r\n        .value {\r\n            width: 60%;\r\n            display: flex;\r\n            justify-content: flex-end;\r\n        }\r\n\r\n        .report {\r\n            display: flex;\r\n            flex-direction: column;\r\n        }\r\n\r\n        .admittance {\r\n            margin-bottom: 3rem;\r\n            display: flex;\r\n            flex-direction: column;\r\n            font-size: 2rem;\r\n            word-wrap: break-word;\r\n        }\r\n\r\n        .adm-header {\r\n            font-weight: bold;\r\n            margin-bottom: 1.5rem;\r\n        }\r\n\r\n        .adm-text {\r\n            font-size: 1.5rem;\r\n        }\r\n\r\n        .therapies {\r\n            display: flex;\r\n            flex-direction: column;\r\n            margin-bottom: 10rem;\r\n        }\r\n\r\n        .therapies-header {\r\n            font-size: 2rem;\r\n            font-weight: bold;\r\n            margin-bottom: 1.5rem;\r\n        }\r\n\r\n        .terapija {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n            padding: 0rem 3rem;\r\n            margin-bottom: 1rem;\r\n        }\r\n\r\n        .naziv-leka {\r\n            font-size: 1.5rem;\r\n            font-weight: 300;\r\n            display: flex;\r\n            align-items: center;\r\n        }\r\n\r\n        .ime {\r\n            font-weight: 600;\r\n            margin-right: 1.5rem;\r\n        }\r\n\r\n        .datum-leka {\r\n            font-size: 1.5rem;\r\n        }\r\n\r\n        .footer {\r\n            display: flex;\r\n            justify-content: space-between;\r\n        }\r\n\r\n        .qr-code {\r\n            background-image: url('./qr.png');\r\n            background-repeat: no-repeat;\r\n            background-position: center;\r\n            background-size: cover;\r\n            height: 10rem;\r\n            width: 10rem;\r\n        }\r\n\r\n        .qr {\r\n            height: 10rem;\r\n            width: 10rem;\r\n        }\r\n\r\n        .signature {\r\n            display: flex;\r\n            flex-direction: column;\r\n            align-self: flex-end;\r\n        }\r\n\r\n        .line {\r\n            border-bottom: 1px solid black;\r\n            width: 15rem;\r\n        }\r\n\r\n        .sig-text {\r\n            font-size: 1.3rem;\r\n            font-style: italic;\r\n            display: flex;\r\n            justify-content: center;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"header\">\r\n        <div class=\"logo-container\">\r\n            <img src=\"https://www.freepnglogos.com/uploads/medicine-logo-png-1.png\" alt=\"\" class=\"log\">\r\n        </div>\r\n        <div class=\"address\">\r\n            <p>PSW Hospital</p>\r\n            <p>Gunduliceva 1,</p>\r\n            <p>21000 Novi Sad, Srbija</p>\r\n            <p>021/555-333</p>\r\n        </div>\r\n    </div>\r\n    <div class=\"main-content\">\r\n        <div class=\"header-text\">\r\n            <h1 class=\"head\">ANAMNESIS REPORT</h1>\r\n            <h3 class=\"date\">START_DATE - END_DATE</h3>\r\n        </div>\r\n        <div class=\"patient-info\">\r\n            <div class=\"col\">\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Name: </div>\r\n                    <div class=\"value\">NAME</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Date of Birth: </div>\r\n                    <div class=\"value\">07.12.1969.</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Address:</div>\r\n                    <div class=\"value\">Gunduliceva 18</div>\r\n                </div>\r\n            </div>\r\n            <div class=\"col\">\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Surname:</div>\r\n                    <div class=\"value\">SURNAME</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">SSN: </div>\r\n                    <div class=\"value\">0712196913120</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Blood Type: </div>\r\n                    <div class=\"value\">BLOOD_TYPE</div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"report\">\r\n            <div class=\"admittance\">\r\n                <div class=\"adm-header\">Anamnesis description: </div>\r\n                <div class=\"adm-text\">ANAMNESIS_DESCRIPTION</div>\r\n            </div>\r\n            <div class=\"admittance\">\r\n                <div class=\"adm-header\">Symptoms: </div>\r\n                <div class=\"adm-text\">SYMPTOMS</div>\r\n            </div>\r\n        </div>\r\n        <div class=\"therapies\">\r\n            <div class=\"therapies-header\">\r\n                Recepies:\r\n            </div>\r\n            PRESCRIPTIONS_LIST\r\n        </div>\r\n    </div>\r\n    <div class=\"footer\">\r\n        <div class=\"qr-code\">\r\n            <img src=\"https://www.qrcode-monkey.com/img/default-preview-qr.svg\" alt=\"\" class=\"qr\">\r\n        </div>\r\n        <div class=\"signature\">\r\n            <div class=\"line\"></div>\r\n            <div class=\"sig-text\">signature</div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
        }
        private static string PdfTemplate()
        {
            return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n    <style>\r\n        html {\r\n            font-size: 10px;\r\n        }\r\n\r\n        body {\r\n        }\r\n\r\n        .header {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n            margin-top: 0rem;\r\n        }\r\n\r\n        .logo-container {\r\n            /*background-image: url('./logo.png');\r\n            background-repeat: no-repeat;\r\n            background-position: center;\r\n            background-size: cover;*/\r\n            height: 10rem;\r\n            width: 10rem;\r\n            padding: 0rem;\r\n        }\r\n\r\n        .log {\r\n            height: 10rem;\r\n            width: 10rem;\r\n        }\r\n\r\n        .address {\r\n            font-size: 1.3rem;\r\n            line-height: 1rem;\r\n        }\r\n\r\n        .address p {\r\n            display: flex;\r\n            justify-content: flex-end;\r\n        }\r\n\r\n        .main-content {\r\n            display: flex;\r\n            justify-content: center;\r\n            flex-direction: column;\r\n        }\r\n\r\n        .header-text {\r\n            margin-bottom: 2.5rem;\r\n        }\r\n\r\n        .head {\r\n            font-size: 3rem;\r\n            display: flex;\r\n            justify-content: center;\r\n        }\r\n\r\n        .date {\r\n            font-size: 2.3rem;\r\n            display: flex;\r\n            justify-content: center;\r\n            font-weight: 500;\r\n        }\r\n\r\n        .patient-info {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            padding: 0rem 1rem;\r\n            margin-bottom: 3rem;\r\n        }\r\n\r\n        .col {\r\n            display: flex;\r\n            flex-direction: column;\r\n            justify-content: flex-start;\r\n            width: 45%;\r\n        }\r\n\r\n        .stavka {\r\n            margin-bottom: 2rem;\r\n            display: flex;\r\n            justify-content: space-between;\r\n            font-size: 1.5rem;\r\n        }\r\n\r\n        .label {\r\n            width: 40%;\r\n            font-weight: 600;\r\n        }\r\n\r\n        .value {\r\n            width: 60%;\r\n            display: flex;\r\n            justify-content: flex-end;\r\n        }\r\n\r\n        .report {\r\n            display: flex;\r\n            flex-direction: column;\r\n        }\r\n\r\n        .admittance {\r\n            margin-bottom: 3rem;\r\n            display: flex;\r\n            flex-direction: column;\r\n            font-size: 2rem;\r\n            word-wrap: break-word;\r\n        }\r\n\r\n        .adm-header {\r\n            font-weight: bold;\r\n            margin-bottom: 1.5rem;\r\n        }\r\n\r\n        .adm-text {\r\n            font-size: 1.5rem;\r\n        }\r\n\r\n        .therapies {\r\n            display: flex;\r\n            flex-direction: column;\r\n            margin-bottom: 10rem;\r\n        }\r\n\r\n        .therapies-header {\r\n            font-size: 2rem;\r\n            font-weight: bold;\r\n            margin-bottom: 1.5rem;\r\n        }\r\n\r\n        .terapija {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n            padding: 0rem 3rem;\r\n            margin-bottom: 1rem;\r\n        }\r\n\r\n        .naziv-leka {\r\n            font-size: 1.5rem;\r\n            font-weight: 300;\r\n            display: flex;\r\n            align-items: center;\r\n        }\r\n\r\n        .ime {\r\n            font-weight: 600;\r\n            margin-right: 1.5rem;\r\n        }\r\n\r\n        .datum-leka {\r\n            font-size: 1.5rem;\r\n        }\r\n\r\n        .footer {\r\n            display: flex;\r\n            justify-content: space-between;\r\n        }\r\n\r\n        .qr-code {\r\n            background-image: url('./qr.png');\r\n            background-repeat: no-repeat;\r\n            background-position: center;\r\n            background-size: cover;\r\n            height: 10rem;\r\n            width: 10rem;\r\n        }\r\n\r\n        .qr {\r\n            height: 10rem;\r\n            width: 10rem;\r\n        }\r\n\r\n        .signature {\r\n            display: flex;\r\n            flex-direction: column;\r\n            align-self: flex-end;\r\n        }\r\n\r\n        .line {\r\n            border-bottom: 1px solid black;\r\n            width: 15rem;\r\n        }\r\n\r\n        .sig-text {\r\n            font-size: 1.3rem;\r\n            font-style: italic;\r\n            display: flex;\r\n            justify-content: center;\r\n        }\r\n\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"header\">\r\n        <div class=\"logo-container\">\r\n            <img src=\"https://www.freepnglogos.com/uploads/medicine-logo-png-1.png\" alt=\"\" class=\"log\">\r\n        </div>\r\n        <div class=\"address\">\r\n            <p>PSW Hospital</p>\r\n            <p>Gunduliceva 1,</p>\r\n            <p>21000 Novi Sad, Srbija</p>\r\n            <p>021/555-333</p>\r\n        </div>\r\n    </div>\r\n    <div class=\"main-content\">\r\n        <div class=\"header-text\">\r\n            <h1 class=\"head\">TREATMENT REPORT</h1>\r\n            <h3 class=\"date\">START_DATE - END_DATE</h3>\r\n        </div>\r\n        <div class=\"patient-info\">\r\n            <div class=\"col\">\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Name: </div>\r\n                    <div class=\"value\">NAME</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Date of Birth: </div>\r\n                    <div class=\"value\">07.12.1969.</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Address:</div>\r\n                    <div class=\"value\">Gunduliceva 18</div>\r\n                </div>\r\n            </div>\r\n            <div class=\"col\">\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Surname:</div>\r\n                    <div class=\"value\">SURNAME</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">SSN: </div>\r\n                    <div class=\"value\">0712196913120</div>\r\n                </div>\r\n                <div class=\"stavka\">\r\n                    <div class=\"label\">Blood Type: </div>\r\n                    <div class=\"value\">BLOOD_TYPE</div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"report\">\r\n            <div class=\"admittance\">\r\n                <div class=\"adm-header\">Admittance reason: </div>\r\n                <div class=\"adm-text\">ADMITTANCE_REASON</div>\r\n            </div>\r\n            <div class=\"admittance\">\r\n                <div class=\"adm-header\">Release report: </div>\r\n                <div class=\"adm-text\">REPORT_Z</div>\r\n            </div>\r\n        </div>\r\n        <div class=\"therapies\">\r\n            <div class=\"therapies-header\">\r\n                Therapies:\r\n            </div>\r\n            THERAPIES_LIST\r\n        </div>\r\n    </div>\r\n    <div class=\"footer\">\r\n        <div class=\"qr-code\">\r\n            <img src=\"https://www.qrcode-monkey.com/img/default-preview-qr.svg\" alt=\"\" class=\"qr\">\r\n        </div>\r\n        <div class=\"signature\">\r\n            <div class=\"line\"></div>\r\n            <div class=\"sig-text\">signature</div>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
        }
    }
}
