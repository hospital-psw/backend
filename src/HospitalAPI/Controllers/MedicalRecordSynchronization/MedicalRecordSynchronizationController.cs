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

    [ApiController]
    [Route("medical=record")]
    public class MedicalRecordSynchronizationController : BaseController<MedicalTreatment>
    {
        private readonly  IMedicalRecordSynchronizationService _mediicalRecordSynchronizationService;

        public MedicalRecordSynchronizationController(IMedicalRecordSynchronizationService mediicalRecordSynchronizationService)
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
