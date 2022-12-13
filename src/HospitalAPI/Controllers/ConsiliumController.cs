namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Consilium;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.Consilium;
    using HospitalLibrary.Core.DTO.Consilium;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Exceptions;
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
            try
            {
                if (dto == null)
                {
                    return BadRequest("Please pass valid data.");
                }
                if (dto.Topic == default(string) || dto.Duration == default(int))
                {
                    return BadRequest("Please pass valid data.");
                }

                Consilium consilium = _doctorScheduleService.TryToScheduleConsilium(dto);
                return Ok(ConsiliumMapper.EntityToDto(_consiliumService.Schedule(consilium)));
            }
            catch (ScheduleConsiliumException e)
            {
                return BadRequest(e.Message);
            }
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

        [HttpGet("room/{roomId}")]
        public IActionResult GetAllForRoom(int roomId)
        {
            List<ConsiliumDto> consiliumDtos = new List<ConsiliumDto>();
            foreach (Consilium consilium in _consiliumService.GetAllForRoom(roomId))
            {
                consiliumDtos.Add(ConsiliumMapper.EntityToDto(consilium));
            }
            return Ok(consiliumDtos);
        }

    }
}
