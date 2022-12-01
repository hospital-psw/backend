namespace HospitalAPI.Controllers.Examinations
{
    using HospitalAPI.Dto.Examinations;
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalAPI.Mappers.Examinations;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using HospitalLibrary.Core.DTO.Examinations;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class AnamnesisController : BaseController<Anamnesis>
    {
        private readonly IAnamnesisService _anamnesisService;

        public AnamnesisController(IAnamnesisService anamnesisService)
        {
            _anamnesisService = anamnesisService;
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
                Anamnesis anamnesis = _anamnesisService.Add(dto);
                return Ok("Uspeo sam");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
