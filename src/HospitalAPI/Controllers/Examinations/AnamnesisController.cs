namespace HospitalAPI.Controllers.Examinations
{
    using HospitalAPI.Mappers.Examinations;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

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
    }
}
