namespace IntegrationAPI.Controllers
{
    using AutoMapper;
    using IntegrationAPI.DTO.Notification;
    using IntegrationAPI.DTO.Tender;
    using IntegrationLibrary.Notification;
    using IntegrationLibrary.Notification.Interfaces;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            Notification entity = _notificationService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NotificationDTO>(entity));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<NotificationDTO>>(_notificationService.GetAll()));
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] NotificationDTO notification)
        {
            var entity = _notificationService.Create(_mapper.Map<Notification>(notification));

            if (entity is null)
            {
                return BadRequest();
            }
            return Ok();
        }




    }
}
