namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;

    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class AllergiesController : BaseController<Allergies>
    {
        private IAllergiesService _allergiesService;

        public AllergiesController(IAllergiesService allergiesService) : base()
        {
            _allergiesService = allergiesService;
        }

        [HttpPost]
        public IActionResult Add(AllergiesDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dto is null, please check your input.");
            }

            return Ok(_allergiesService.Add(AllergiesMapper.EntityDtoToEntity(dto)));



        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<AllergiesDto> allergiesDto = new List<AllergiesDto>();
            List<Allergies> allergies = _allergiesService.GetAll().ToList();
            if (allergies == null)
            {
                return NotFound();
            }
            allergies.ForEach(a => allergiesDto.Add(AllergiesMapper.EntityToEntityDto(a)));
            return Ok(allergiesDto);
        }

    }
}
