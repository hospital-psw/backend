namespace HospitalAPI.Controllers.MedicalRecordSynchronization
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Service.MedicalRecordSynchronization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.ObjectPool;
    using System;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalRecordSynchronizationController : BaseController<MedicalTreatment>
    {
        private readonly MedicalRecordSynchronizationService _mediicalRecordSynchronizationService;

        public MedicalRecordSynchronizationController(MedicalRecordSynchronizationService mediicalRecordSynchronizationService)
        {
            _mediicalRecordSynchronizationService = mediicalRecordSynchronizationService;
        }

        [HttpGet("/{patientId}")]
        public Task<string> GetPreviousMedicalRecord(int patientId) 
        {
            return _mediicalRecordSynchronizationService.GetPreviousMedicalRecord(patientId);
        }
    }
}
