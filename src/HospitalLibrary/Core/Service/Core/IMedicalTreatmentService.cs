namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMedicalTreatmentService
    {

        MedicalTreatment Get(int id);

        MedicalTreatment Update(MedicalTreatment medicalTreatment);

        IEnumerable<MedicalTreatment> GetAll();

        MedicalTreatment Add(NewMedicalTreatmentDto dto);

        void Delete(MedicalTreatment medicalTreatment);
        MedicalTreatment ReleasePatient(MedicalTreatment medicalTreatment, string description);
    }
}
