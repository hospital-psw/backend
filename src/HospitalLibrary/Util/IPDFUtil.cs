namespace HospitalLibrary.Util
{
    using HospitalLibrary.Core.DTO.PDF;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPDFUtil
    {
        Stream GenerateTreatmentPdf(MedicalTreatment treatment);
        Stream GenerateAnamnesisPDF(Anamnesis anamnesis, AnamnesisPdfDTO dto);
    }
}
