namespace HospitalAPI.Controllers
{
    using AutoMapper;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
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
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;

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

            return Ok(AppointmentMapper.EntityToEntityDto(appointment));
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

            if ((appointment.Date - DateTime.Now).TotalDays < 2)
            {
                return BadRequest("You can't reschedule this appointment.");
            }
            if (Math.Abs((dto.Date - appointment.Date).TotalDays) > 4)
            {
                return BadRequest("Appointment can be rescheduled only 4 days before or after of current date.");
            }
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
        [Route("doctor/{id}")]
        public IActionResult GetDoctorAppointments(int id)
        {
            List<Appointment> appointments = (List<Appointment>)_appointmentService.GetByDoctorsId(id);

            if (appointments.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(AppointmentMapper.EntityListToEntityDtoList(appointments));
        }

        [HttpGet]
        [Route("room/{id}")]
        public IActionResult GetAllForRoom(int id)
        {
            List<AppointmentDisplayDto> appointmentDtos = new List<AppointmentDisplayDto>();
            foreach(Appointment appointment in _appointmentService.GetAllForRoom(id))
            {
                appointmentDtos.Add(AppointmentDisplayMapper.EntityToEntityDto(appointment));
            }
            return Ok(appointmentDtos);
        }
    }
}
