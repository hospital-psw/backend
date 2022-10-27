namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.EmailServices;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : BaseController<Appointment>
    {
        private IAppointmentService _appointmentService;
        private IEmailService _emailService;
        public AppointmentController(IAppointmentService appointmentService, IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Appointment appointment = _appointmentService.Get(id);
            if (appointment == null)
            {
                return NotFound();
            }
            AppointmentDto dto = AppointmentMapper.EntityToEntityDto(appointment);
            return Ok(dto);
        }

        [HttpPut]
        public IActionResult Update(RescheduleAppointmentDto dto)
        {
            if (dto == null)
            {
                return NotFound();
            }

            Appointment appointment = RescheduleAppointmentMapper.EntityDtoToEntity(dto);
            return Ok(_appointmentService.Update(appointment));
        }

        [HttpGet]
        [Route("/send")]
        public IActionResult SendEmail()
        {
            _emailService.Send();
            return Ok();
        }
    }
}
