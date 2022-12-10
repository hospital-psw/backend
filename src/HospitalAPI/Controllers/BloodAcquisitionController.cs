namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.Blood;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Blood;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodAcquisitionController : BaseController<BloodAcquisition>
    {
        private IBloodAcquisitionService bloodAcquisitionService;
        private readonly IApplicationDoctorService _doctorService;

        public BloodAcquisitionController(IBloodAcquisitionService _bloodAcquisitionService, IApplicationDoctorService doctorService)
        {
            bloodAcquisitionService = _bloodAcquisitionService;
            _doctorService = doctorService;
        }

        [HttpPatch("/handle")]
        public IActionResult HandleBloodAcquisition([FromBody] EditAcquisitionDTO acquisition)
        {
            bloodAcquisitionService.HandleBloodRequest(acquisition.Status, acquisition.Id, acquisition.ManagerComment);
            return Ok();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(bloodAcquisitionService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BloodAcquisition bloodAcquisition = bloodAcquisitionService.Get(id);
            if (bloodAcquisition == null)
            {
                return BadRequest("Id does not exist");
            }
            else
                return Ok(bloodAcquisition);
        }


        [HttpPost]
        public IActionResult Create(CreateAcquisitionDTO createAcquisitionDTO)
        {
            if (createAcquisitionDTO == null)
            {
                return BadRequest("Incorrect data, please enter valid data");
            }
            if (createAcquisitionDTO.BloodType < 0 || createAcquisitionDTO.Reason == null || createAcquisitionDTO.Amount < 1 || createAcquisitionDTO.Date == default(DateTime))
            {
                return BadRequest("Please enter valid data");
            }
            if (_doctorService.Get(createAcquisitionDTO.DoctorId) == null)
            {
                return BadRequest("Doctor not found");
            }
            else
                bloodAcquisitionService.Create(createAcquisitionDTO);
            return Ok(createAcquisitionDTO);
        }

        [HttpGet("/pending")]
        public IActionResult GetPendingBloodAcquisitionRequests()
        {
            return Ok(bloodAcquisitionService.GetPendingAcquisitions());
        }

        [HttpGet("get/all/accepted")]
        public IActionResult GetAllAccepted()
        {
            return Ok(bloodAcquisitionService.GetAllAcceptedAcquisition());
        }

        [HttpGet("get/all/declined")]
        public IActionResult GetAllDeclined()
        {
            return Ok(bloodAcquisitionService.GetAllDeclinedAcquisition());
        }

        [HttpGet("get/all/reconsidering")]
        public IActionResult GetAllReconsidering()
        {
            return Ok(bloodAcquisitionService.GetAllReconsideringAcquisition());
        }

        [HttpPut("/accept/{id}")]
        public IActionResult AcceptBloodAcquisition(int id)
        {
            if (bloodAcquisitionService.Get(id) == null)
            {
                return BadRequest("Id does not exist");
            }
            return Ok(bloodAcquisitionService.AcceptAcquisition(id));
        }

        [HttpPut("/decline/{id}")]
        public IActionResult DeclineBloodAcquisition(int id)
        {
            if (bloodAcquisitionService.Get(id) == null)
            {
                return BadRequest("Id does not exist");
            }
            return Ok(bloodAcquisitionService.DeclineAcquisition(id));
        }

        [HttpPut("/edit")]
        public IActionResult EditBloodRequest(BloodAcquisition bloodAcquisition)
        {
            bloodAcquisition.Status = BloodRequestStatus.PENDING;
            return Ok(bloodAcquisitionService.Update(bloodAcquisition));
        }

        [HttpGet("/doctorAcquisitions/{id}")]
        public IActionResult GetAcquisitionsForSpecificDoctor(int id)
        {
            return Ok(bloodAcquisitionService.GetAcquisitionsForSpecificDoctor(id));
        }
    }
}
