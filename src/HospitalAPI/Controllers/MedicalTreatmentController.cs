namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class MedicalTreatmentController : BaseController<MedicalTreatment>
    {

        private readonly IMedicalTreatmentService _medicalTreatmentService;

        public MedicalTreatmentController(IMedicalTreatmentService medicalTreatmentService)
        {
            _medicalTreatmentService = medicalTreatmentService;
        }
    }
}
