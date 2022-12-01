namespace HospitalAPI.Controllers.Examinations
{
    using HospitalAPI.Dto.Examinations;
    using HospitalAPI.Mappers.Examinations;
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : BaseController<Prescription>
    {
        private IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(PrescriptionMapper.EntityListToEntityDtoList(_prescriptionService.GetAll().ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(PrescriptionMapper.EntityToEntityDto(_prescriptionService.Get(id)));
        }

        [HttpPost]
        public IActionResult Add(NewPrescriptionDto dto)
        {
            if (dto.From > dto.To)
            {
                return BadRequest("Date range must be valid. Start date should be before end date");
            }

            if (string.IsNullOrEmpty(dto.Description))
            {
                return BadRequest("Description should not be empty");
            }

            Prescription prescription = _prescriptionService.Add(dto.MedicamentId, dto.Description, new DateRange(dto.From, dto.To));

            if (prescription == null)
            {
                return BadRequest("Medicament does not exist");
            }

            return Ok(PrescriptionMapper.EntityToEntityDto(prescription));
        }
    }
}
