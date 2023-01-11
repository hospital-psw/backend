namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SymptomDataDto
    {
        public string Symptom { get; set; }

        public int Showings { get; set; }

        public SymptomDataDto()
        {

        }

        public SymptomDataDto(string symptom, int showings)
        {
            Symptom = symptom;
            Showings = showings;
        }
    }
}
