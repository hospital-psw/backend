namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : BaseController<Equipment>
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet("{roomId}")]
        public IActionResult GetForRoom(int roomId)
        {
            List<EquipmentDto> equipmentDto = new List<EquipmentDto>();
            List<Equipment> equipment = _equipmentService.GetForRoom(roomId);
            if (equipment == null)
            {
                return NotFound();
            }
            equipment.ForEach(e => equipmentDto.Add(EquipmentMapper.EntityToEntityDto(e)));
            return Ok(equipmentDto);
        }
    }
}
