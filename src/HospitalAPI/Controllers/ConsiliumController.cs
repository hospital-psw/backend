namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.Consilium;
    using HospitalAPI.Mappers.Consilium;
    using HospitalLibrary.Core.DTO.Consilium;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class ConsiliumController : BaseController<Consilium>
    {
        public IConsiliumService _consiliumService;
        public IDoctorScheduleService _doctorScheduleService;

        public ConsiliumController(IConsiliumService consiliumService, IDoctorScheduleService doctorScheduleService)
        {
            _consiliumService = consiliumService;
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost]
        public IActionResult Schedule(ScheduleConsiliumDto dto)
        {
            if(dto == null)
            {
                return BadRequest("Please pass valid data.");
            }
            if(dto.Topic == default(string) || dto.Duration == default(int) || dto.DateRange == null)
            {
                return BadRequest("Please pass valid data.");
            }

            Consilium consilium = _doctorScheduleService.TryToScheduleConsilium(dto);
            if (consilium == null)
            {
                return BadRequest("Can not schedule consilium in passed date range.");
            }

            return Ok(ConsiliumMapper.EntityToDto(_consiliumService.Schedule(consilium)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Consilium consilium = _consiliumService.Get(id);
            if (consilium == null)
            {
                return NotFound();
            }
            return Ok(ConsiliumMapper.EntityToDto(consilium));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<ConsiliumDto> consiliumDtoList = new List<ConsiliumDto>();
            List<Consilium> consiliumList = _consiliumService.GetAll().ToList();
            if (consiliumList.IsNullOrEmpty())
            {
                return NotFound();
            }
            consiliumList.ForEach(consilium => consiliumDtoList.Add(ConsiliumMapper.EntityToDto(consilium)));
            return Ok(consiliumDtoList);
        }

    }
}
