namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Enum;
    using HospitalAPI.Mappers.Renovation;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class EventController : BaseController<DomainEvent>
    {
        private IRenovationEventService _renovationEventService;
        public EventController(IRenovationEventService renovationEventService) : base()
        {
            _renovationEventService = renovationEventService;
        }

        [HttpPost]
        public IActionResult CreateRenovationEvent(RenovationEventDto dto) {
            return Ok(_renovationEventService.Execute(RenovationEventMapper.EntityDtoToEntity(dto)).AggregateId);
        }

    }
}
