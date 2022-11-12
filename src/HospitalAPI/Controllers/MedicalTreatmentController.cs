namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.MedicamentTreatment;
    using HospitalAPI.Mappers.MedicalTreatment;
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class MedicalTreatmentController : BaseController<MedicalTreatment>
    {

        private readonly IMedicalTreatmentService _medicalTreatmentService;

        public MedicalTreatmentController(IMedicalTreatmentService medicalTreatmentService)
        {
            _medicalTreatmentService = medicalTreatmentService;
        }

        [HttpPost]
        public IActionResult Create(NewMedicalTreatmentDto dto)
        {
            if (dto == null)
            {
                BadRequest("Bad request, please enter valid data.");
            }
            else if (dto.PatientId == default(int) || dto.DoctorId == default(int) || dto.RoomId == default(int))
            {
                BadRequest("Bad request, please enter valid data.");
            }

            return Ok(MedicalTreatmentMapper.EntityToEntityDto(_medicalTreatmentService.Add(dto)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            MedicalTreatment medicalTreatment = _medicalTreatmentService.Get(id);

            if (medicalTreatment == null)
            {
                return NotFound();
            }

            return Ok(MedicalTreatmentMapper.EntityToEntityDto(medicalTreatment));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<MedicalTreatment> medicalTreatments = _medicalTreatmentService.GetAll().ToList();
            List<MedicalTreatmentDto> dtoList = new List<MedicalTreatmentDto>();

            if (medicalTreatments == null)
            {
                return NotFound();
            }

            medicalTreatments.ForEach(mt => dtoList.Add(MedicalTreatmentMapper.EntityToEntityDto(mt)));
            return Ok(dtoList);
        }
    }

}
