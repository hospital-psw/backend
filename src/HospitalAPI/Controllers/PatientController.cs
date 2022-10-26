namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : BaseController<Patient>
    {
        private IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public IActionResult Add(NewPatientDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }

            return Ok(_patientService.Add(NewPatientMapper.EntityDtoToEntity(dto)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int patientId)
        {
            Patient patient = _patientService.Get(patientId);
            if (patient == null)
            {
                return NotFound();
            }
            PatientDto dto = PatientMapper.EntityToEntityDto(patient);
            return Ok(dto);
        }

        [HttpGet("/all")]
        public IActionResult GetAll()
        {
            List<PatientDto> patientsDto = new List<PatientDto>();
            List<Patient> patients = _patientService.GetAll().ToList();
            if (patients == null)
            {
                return NotFound();
            }
            patients.ForEach(p => patientsDto.Add(PatientMapper.EntityToEntityDto(p)));
            return Ok(patientsDto);
        }

        [HttpPut]
        public IActionResult Update(UpdatePatientDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            Patient patient = _patientService.Get(dto.Id);
            if (patient == null || patient.Deleted)
            {
                return NotFound();
            }

            return Ok(_patientService.Update(UpdatePatientMapper.EntityDtoToEntity(dto)));
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(int patientId)
        {
            Patient patient = _patientService.Get(patientId);
            if (patient == null || patient.Deleted)
            {
                return NotFound();
            }

            bool response = _patientService.Delete(patientId);
            if (!response)
            {
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}