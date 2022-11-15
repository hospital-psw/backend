namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.DTO.VacationRequest;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class VacationRequestsController : BaseController<VacationRequest>
    {
        private IVacationRequestsService _vacationRequestsService;
        private IAppointmentService _appointmentService;

        public VacationRequestsController(IVacationRequestsService vacationRequestsService, IAppointmentService appointmentService)
        {
            _vacationRequestsService = vacationRequestsService;
            _appointmentService = appointmentService;
        }

        [HttpGet("getAllPending")]
        public IActionResult GetAllPending()
        {
            List<VacationRequestDto> vacationRequestsDto = new List<VacationRequestDto>();
            List<VacationRequest> vacationRequests = _vacationRequestsService.GetAllPending().ToList();
            if (vacationRequests == null)
            {
                return NotFound();
            }
            vacationRequests.ForEach(r => vacationRequestsDto.Add(VacationRequestsMapper.EntityToEntityDto(r)));
            return Ok(vacationRequestsDto);
        }

        [HttpPost]
        public IActionResult Create(NewVacationRequestDto dto)
        {
            int wrongDates = DateTime.Compare(dto.From, dto.To);

            if (wrongDates > 0)
            {
                return BadRequest();
            }

            List<Appointment> appointments = (List<Appointment>)_appointmentService.GetAppointmentsInDateRangeDoctor(dto.DoctorId, dto.From, dto.To);

            if (!appointments.IsNullOrEmpty())
            {
                return BadRequest();
            }

            return Ok(VacationRequestsMapper.EntityToEntityDto(_vacationRequestsService.Create(dto)));
        }
    }
}
