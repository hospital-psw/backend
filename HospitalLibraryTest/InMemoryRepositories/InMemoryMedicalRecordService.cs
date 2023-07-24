namespace HospitalLibraryTest.InMemoryRepositories
{
    using HospitalLibrary.Core.Service.MedicalRecordSynchronization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InMemoryMedicalRecordService : IMedicalRecordSynchronizationService
    {
        public Task<string> GetPreviousMedicalRecord(int patientId)
        {
            return Task.FromResult("mokovani testni rekord");
        }
    }
}
