﻿namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class VacationRequestsController : BaseController<VacationRequest>
    {
        private IVacationRequestsService _vacationRequestsService;

        public VacationRequestsController(IVacationRequestsService vacationRequestsService)
        {
            _vacationRequestsService = vacationRequestsService;
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
    }
}