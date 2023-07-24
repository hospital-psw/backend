namespace HospitalLibrary.Core.Service.MedicalRecordSynchronization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMedicalRecordSynchronizationService
    {
        Task<string> GetPreviousMedicalRecord(int patientId);
    }
}
