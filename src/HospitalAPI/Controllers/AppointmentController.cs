namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : BaseController<Appointment>
    {
        private IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            AppointmentDto appointment = AppointmentMapper.EntityToEntityDto(_appointmentService.Get(id));
            return appointment is null ? NotFound() : Ok(appointment);
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
    }
}