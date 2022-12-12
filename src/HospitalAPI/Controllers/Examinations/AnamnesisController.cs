namespace HospitalAPI.Controllers.Examinations
{
    using HospitalAPI.Dto.Examinations;
    using HospitalAPI.Mappers.Examinations;
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class AnamnesisController : BaseController<Anamnesis>
    {
        private readonly IAnamnesisService _anamnesisService;
        private readonly IPrescriptionService _prescriptionService;

        public AnamnesisController(IAnamnesisService anamnesisService, IPrescriptionService prescriptionService)
        {
            _anamnesisService = anamnesisService;
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(AnamnesisMapper.EntityListToEntityDtoList(_anamnesisService.GetAll().ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_anamnesisService.Get(id)));
        }

        [HttpPost]
        public IActionResult Add(NewAnamnesisDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Description))
                {
                    return BadRequest("Please enter anamnesis report");
                }

                Anamnesis anamnesis = _anamnesisService.Add(dto);
                List<Prescription> prescriptions = null;

                if (dto.NewPrescriptions != null)
                {
                    prescriptions = _prescriptionService.AddMultiple(dto.NewPrescriptions);
                    _anamnesisService.AddPrescriptions(anamnesis.Id, prescriptions);
                }

                return Ok(AnamnesisMapper.EntityToEntityDto(anamnesis));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
