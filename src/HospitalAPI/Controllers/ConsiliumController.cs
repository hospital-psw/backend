namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
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

        public ConsiliumController(IConsiliumService consiliumService)
        {
            _consiliumService = consiliumService;
        }

        //public IActionResult Schedule(NewConsiliumDto dto)
        //{

        //}

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Consilium consilium = _consiliumService.Get(id);
            if (consilium == null)
            {
                return NotFound();
            }
            //ConsiliumDto dto = 
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            //List<ConsiliumDto> consiliumDtoList = new List<ConsiliumDto>();
            List<Consilium> consiliumList = _consiliumService.GetAll().ToList();
            if (consiliumList.IsNullOrEmpty())
            {
                return NotFound();
            }
            //consiliumList.ForEach(consilium => consiliumDtoList.Add());
            return Ok();
        }
        [HttpGet("room/{roomId}")]
        public IActionResult GetAllForRoom(int roomId)
        {
            List<ConsiliumDisplayDto> consiliumDtos = new List<ConsiliumDisplayDto>();
            foreach (Consilium consilium in _consiliumService.GetAllForRoom(roomId))
            {
                consiliumDtos.Add(ConsiliumDisplayMapper.EntityToEntityDto(consilium));
            }
            return Ok(consiliumDtos);
        }

    }
}
