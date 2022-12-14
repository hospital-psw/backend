namespace HospitalAPI.Controllers
{
    using AutoMapper;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPI.EmailServices;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : BaseController<Appointment>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IApplicationPatientService _patientService;

        public AppointmentController(IAppointmentService appointmentService,
            IEmailService emailService, 
            IDoctorScheduleService doctorScheduleService,
            IApplicationPatientService patientService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
            _doctorScheduleService = doctorScheduleService;
            _patientService = patientService;
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
            return Ok(_doctorScheduleService.RecommendAppointments(dto));
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
            foreach (Appointment appointment in _appointmentService.GetAllForRoom(id))
            {
                appointmentDtos.Add(AppointmentDisplayMapper.EntityToEntityDto(appointment));
            }
            return Ok(appointmentDtos);
        }

        [HttpGet("patient/{id}")]
        public IActionResult GetPatientAppointments(int id)
        {
            var patient = _patientService.Get(id);
            if (patient == null)
                return NotFound("Patient with this id, doesn't exists in our system.");

            var appointments = _appointmentService.GetByPatientsId(id).ToList();
            if (appointments == null)
                return Ok("There are no appointments for this patient.");

            return Ok(AppointmentMapper.EntityListToEntityDtoList(appointments));
        }


    }
}
