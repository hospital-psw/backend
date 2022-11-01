namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.EmailServices;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : BaseController<Appointment>
    {
        private IAppointmentService _appointmentService;
        private IEmailService _emailService;
        private IDoctorService _doctorService;

        public AppointmentController(IAppointmentService appointmentService, IEmailService emailService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
            _doctorService = doctorService;
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
            else if (dto.Id == default(int) || dto.Date == default(DateTime))
            {
                return BadRequest("Please enter valid data.");
            }

            Appointment appointment = _appointmentService.Get(dto.Id);
            return Ok(_appointmentService.Update(RescheduleAppointmentMapper.EntityDtoToEntity(dto, appointment)));
        }

        [HttpGet]
        [Route("send")]
        public IActionResult SendEmail(Appointment appointment)
        {
            _emailService.Send(appointment);
            return Ok();
        }

        [HttpPost]
        [Route("recommend")]
        public IActionResult RecommendAppointments(RecommendRequestDto dto)
        {
            return Ok(_appointmentService.RecommendAppointments(dto));
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(NewAppointmentDto dto)
        {
            return Ok(_appointmentService.Create(dto));
        }

        [HttpDelete]
        [Route("cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            var appointment = _appointmentService.Get(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _emailService.Send(appointment);

            _appointmentService.Delete(appointment);
            return Ok();
        }

        [HttpGet]
        [Route("doctor/{doctorId}")]
        public IActionResult GetByDoctorId(int doctorId)
        {
            var doctor = _doctorService.Get(doctorId);

            if (doctor == null)
            {
                return BadRequest();
            }

            var appointments = _appointmentService.GetByDoctorsId(doctorId);

            if (appointments.IsNullOrEmpty())
            {
                return NoContent();
            }

            var appointmentsDTO = new List<AppointmentDto>();
            foreach (Appointment a in appointments)
            {
                appointmentsDTO.Add(AppointmentMapper.EntityToEntityDto(a));
            }

            return Ok(appointmentsDTO);
        }
    }
}
