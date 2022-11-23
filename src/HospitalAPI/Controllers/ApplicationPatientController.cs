namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]")]
    public class ApplicationPatientController : BaseController<ApplicationUser>
    {
        private IApplicationPatientService _patientService;

        public ApplicationPatientController(IApplicationPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ApplicationPatient patient = _patientService.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            ApplicationPatientDto dto = ApplicationPatientMapper.EntityToEntityDto(patient);
            return Ok(patient);
        }
    }
}
