﻿namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.Therapy;
    using HospitalAPI.Mappers.Therapy;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodUnitTherapyController : BaseController<BloodUnitTherapy>
    {

        private readonly IBloodUnitTherapyService _bloodUnitTherapyService;

        public BloodUnitTherapyController(IBloodUnitTherapyService bloodUnitTherapyService)
        {
            _bloodUnitTherapyService = bloodUnitTherapyService;
        }

        [HttpPost]
        public IActionResult Create(NewBloodUnitTherapyDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            else if (dto.BloodUnitId == default(int) || dto.Amount == default(int) || dto.About == default(string))
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            //fali poziv servisa da se dobavi BloodUnit
            return Ok(_bloodUnitTherapyService.Add(NewBloodUnitTherapyMapper.EntityDtoToEntity(dto)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BloodUnitTherapy bloodUnitTherapy = _bloodUnitTherapyService.Get(id);

            if (bloodUnitTherapy == null)
            {
                return NotFound();
            }

            return Ok(BloodUnitTherapyMapper.EntityToEntityDto(bloodUnitTherapy));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<BloodUnitTherapy> bloodUnitTherapies = _bloodUnitTherapyService.GetAll().ToList();
            List<BloodUnitTherapyDto> dtoList = new List<BloodUnitTherapyDto>();

            if (bloodUnitTherapies == null)
            {
                return NotFound();
            }

            bloodUnitTherapies.ForEach(bt => dtoList.Add(BloodUnitTherapyMapper.EntityToEntityDto(bt)));
            return Ok(dtoList);
        }
    }
}
