namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.DTO.VacationRequest;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        [HttpPatch("handle")]
        public IActionResult HandleVacationRequest([FromBody] VacationRequestDto request)
        {
            _vacationRequestsService.HandleVacationRequest(request.Status, request.Id, request.ManagerComment);
            return Ok();
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

            if ((dto.From - DateTime.Now).TotalDays < 5 && !dto.Urgent)
            {
                return BadRequest("Request for vacation must be submitted at least 5 days before start date");
            }

            VacationRequest request = _vacationRequestsService.Create(dto);

            if (request == null)
            {
                return BadRequest("You have scheduled appointments in given date interval");
            }

            return Ok(VacationRequestsMapper.EntityToEntityDto(request));
        }
        [HttpGet("{id}")]
        public IActionResult GetAllRequestsByDoctorId(int id)
        {
            List<VacationRequest> vacationRequests = (List<VacationRequest>)_vacationRequestsService.GetAllRequestsByDoctorId(id);

            if (vacationRequests.IsNullOrEmpty())
            {
                return NotFound();
            }

            List<VacationRequestDto> dtos = new List<VacationRequestDto>();

            vacationRequests.ForEach(req => dtos.Add(VacationRequestsMapper.EntityToEntityDto(req)));

            return Ok(dtos);
        }
        [HttpGet("waiting/{id}")]
        public IActionResult GetAllWaitingByDoctorId(int id)
        {
            List<VacationRequest> vacationRequests = (List<VacationRequest>)_vacationRequestsService.GetAllWaitingByDoctorId(id);
            if (vacationRequests.IsNullOrEmpty())
            {
                return NotFound();
            }

            List<VacationRequestDto> dtos = new List<VacationRequestDto>();

            vacationRequests.ForEach(req => dtos.Add(VacationRequestsMapper.EntityToEntityDto(req)));

            return Ok(dtos);
        }
        [HttpGet("approved/{id}")]
        public IActionResult GetAllApprovedByDoctorId(int id)
        {
            List<VacationRequest> vacationRequests = (List<VacationRequest>)_vacationRequestsService.getAllApprovedByDoctorId(id);
            if (vacationRequests.IsNullOrEmpty())
            {
                return NotFound();
            }

            List<VacationRequestDto> dtos = new List<VacationRequestDto>();

            vacationRequests.ForEach(req => dtos.Add(VacationRequestsMapper.EntityToEntityDto(req)));

            return Ok(dtos);
        }

        [HttpGet("rejected/{id}")]
        public IActionResult GetAllRejectedByDoctorId(int id)
        {
            List<VacationRequest> vacationRequests = (List<VacationRequest>)_vacationRequestsService.GetAllRejectedByDoctorId(id);
            if (vacationRequests.IsNullOrEmpty())
            {
                return NotFound();
            }

            List<VacationRequestDto> dtos = new List<VacationRequestDto>();

            vacationRequests.ForEach(req => dtos.Add(VacationRequestsMapper.EntityToEntityDto(req)));

            return Ok(dtos);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            VacationRequest vr = _vacationRequestsService.GetById(id);

            if (vr == null)
            {
                return NotFound();
            }

            if (vr.Status != VacationRequestStatus.WAITING)
            {
                return BadRequest();
            }

            _vacationRequestsService.Delete(vr);

            return Ok();
        }
    }
}
