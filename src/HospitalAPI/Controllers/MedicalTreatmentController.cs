﻿namespace HospitalAPI.Controllers
{
    using AutoMapper;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPI.Dto.MedicamentTreatment;
    using HospitalAPI.Mappers.MedicalTreatment;
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class MedicalTreatmentController : BaseController<MedicalTreatment>
    {
        private readonly IMedicalTreatmentService _medicalTreatmentService;

        public MedicalTreatmentController(IMedicalTreatmentService medicalTreatmentService)
        {
            _medicalTreatmentService = medicalTreatmentService;
        }

        [HttpPost]
        public IActionResult Create(NewMedicalTreatmentDto dto)
        {
            if (dto == null)
            {
                BadRequest("Bad request, please enter valid data.");
            }
            else if (dto.PatientId == default(int) || dto.DoctorId == default(int) || dto.RoomId == default(int))
            {
                BadRequest("Bad request, please enter valid data.");
            }

            return Ok(MedicalTreatmentMapper.EntityToEntityDto(_medicalTreatmentService.Add(dto)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            MedicalTreatment medicalTreatment = _medicalTreatmentService.Get(id);

            if (medicalTreatment == null)
            {
                return NotFound();
            }

            return Ok(MedicalTreatmentMapper.EntityToEntityDto(medicalTreatment));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<MedicalTreatment> medicalTreatments = _medicalTreatmentService.GetAll().ToList();
            List<MedicalTreatmentDto> dtoList = new List<MedicalTreatmentDto>();

            if (medicalTreatments == null)
            {
                return NotFound();
            }

            medicalTreatments.ForEach(mt => dtoList.Add(MedicalTreatmentMapper.EntityToEntityDto(mt)));
            return Ok(dtoList);
        }

        [HttpPatch]
        public IActionResult ReleasePatient(PatientReleaseDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid request");
            }

            if (string.IsNullOrEmpty(dto.Description))
            {
                return BadRequest("Please specify reason for release");
            }

            MedicalTreatment treatment = _medicalTreatmentService.Get(dto.TreatmentId);

            if (treatment == null)
            {
                return NotFound("Treatment not found");
            }

            if (!treatment.Active)
            {
                return BadRequest("Treatment is already finished");
            }

            MedicalTreatment finishedTreatment = _medicalTreatmentService.ReleasePatient(treatment, dto.Description);

            return Ok(MedicalTreatmentMapper.EntityToEntityDto(finishedTreatment));
        }

        [HttpGet("active/{doctorId}/{pageSize}/{pageNumber}")]
        public IActionResult GetDoctorsActiveTreatments(int doctorId, int pageSize, int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            IEnumerable<MedicalTreatment> activeTreatments = _medicalTreatmentService.GetDoctorsActiveTreatments(doctorId);
            List<MedicalTreatmentDto> dtoList = new List<MedicalTreatmentDto>();

            if (activeTreatments.IsNullOrEmpty())
            {
                return NotFound();
            }

            activeTreatments.ToList().ForEach(t => dtoList.Add(MedicalTreatmentMapper.EntityToEntityDto(t)));
            return Ok(dtoList.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet("inactive/{doctorId}/{pageSize}/{pageNumber}")]
        public IActionResult GetDoctorsInactiveTreamtents(int doctorId, int pageSize, int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            IEnumerable<MedicalTreatment> inactiveTreatments = _medicalTreatmentService.GetDoctorsInactiveTreatments(doctorId);
            List<MedicalTreatmentDto> dtoList = new List<MedicalTreatmentDto>();

            if (inactiveTreatments.IsNullOrEmpty())
            {
                return NotFound();
            }

            inactiveTreatments.ToList().ForEach(t => dtoList.Add(MedicalTreatmentMapper.EntityToEntityDto(t)));
            return Ok(dtoList.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet("pdf/{id}")]
        public IActionResult FetchPdf(int id)
        {
            _medicalTreatmentService.GeneratePdf(id);

            var stream = new FileStream(@"./../HospitalLibrary/Resources/PDF/treatment.pdf", FileMode.Open);
            return File(stream, "application/pdf", "treatment.pdf");

        }
    }

}
