namespace HospitalLibrary.Core.DTO.RenovationRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationStatisticDto
    {
        public DateTime Date { get; set; }
        public double Step1 { get; set; }
        public double Step2 { get; set; }
        public double Step3 { get; set; }
        public double Step4 { get; set; }
        public double Step5 { get; set; }
        public double Step6 { get; set; }

        public RenovationStatisticDto() { }

        public RenovationStatisticDto(DateTime date, double step1, double step2, double step3, double step4, double step5, double step6) 
        {
            Date = date;
            Step1 = step1;
            Step2 = step2;
            Step3 = step3;
            Step4 = step4;
            Step5 = step5;
            Step6 = step6;
        }

    }
}
