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
        private IRenovationService _renovationService;
        private IRenovationEventService _renovationEventService;
        public EventController(IRenovationService renovationService, IRenovationEventService renovationEventService) : base()
        {
            _renovationService = renovationService;
            _renovationEventService = renovationEventService;
        }

        [HttpPost]
        public IActionResult CreateRenovationEvent(RenovationEventDto dto) {
            if (dto.EventType == RenovationEventType.RENOVATION_TYPE_EVENT) {
                RenovationRequest request = _renovationService.Create(RenovationRequest.Create(dto.Type, null, DateTime.Now, 0, null));
                dto.AggregateId = request.Id;
            }
            return Ok(_renovationEventService.Add(RenovationEventMapper.EntityDtoToEntity(dto)).AggregateId);
        }

    }
}
