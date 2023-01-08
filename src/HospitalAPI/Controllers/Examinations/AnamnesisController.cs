namespace HospitalAPI.Controllers.Examinations
{
    using HospitalAPI.Dto.Examinations;
    using HospitalAPI.Mappers.Examinations;
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.DTO.PDF;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using HospitalLibrary.Util;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    [Route("api/[controller]")]
    [ApiController]
    public class AnamnesisController : BaseController<Anamnesis>
    {
        private readonly IAnamnesisService _anamnesisService;
        private readonly IPrescriptionService _prescriptionService;

        public AnamnesisController(IAnamnesisService anamnesisService, IPrescriptionService prescriptionService)
        {
            _anamnesisService = anamnesisService;
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(AnamnesisMapper.EntityListToEntityDtoList(_anamnesisService.GetAll().ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(AnamnesisMapper.EntityToEntityDto(_anamnesisService.Get(id)));
        }

        [HttpGet("search")]
        public IActionResult Search(string input)
        {
            var anamneses = _anamnesisService.GetAnamnesesBySearchCriteria(SearchParser.Parse(input)).ToList();
            return Ok(AnamnesisMapper.EntityListToEntityDtoList(anamneses));
        }

        [HttpPost]
        public IActionResult Add(NewAnamnesisDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Description))
                {
                    return BadRequest("Please enter anamnesis report");
                }

                Anamnesis anamnesis = _anamnesisService.Add(dto);
                List<Prescription> prescriptions = null;

                if (dto.NewPrescriptions != null)
                {
                    prescriptions = _prescriptionService.AddMultiple(dto.NewPrescriptions);
                    _anamnesisService.AddPrescriptions(anamnesis.Id, prescriptions);
                }
                return Ok(AnamnesisMapper.EntityToEntityDto(anamnesis));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("byAppointment/{id}")]
        public IActionResult GetByAppointment(int id)
        {
            return Ok(_anamnesisService.GetByAppointment(id));
        }

        [HttpPost("pdf")]
        public IActionResult FetchPdf(AnamnesisPdfDTO dto)
        {

            _anamnesisService.GeneratePdf(dto);

            var stream = new FileStream(@"./../HospitalLibrary/Resources/PDF/anamnesis.pdf", FileMode.Open);
            return File(stream, "application/pdf", "anamnesis.pdf");

        }

        [HttpPost("edit")]
        public IActionResult Edit(Anamnesis anamnesis)
        {
            Anamnesis addAnamnesis = _anamnesisService.Get(anamnesis.Id);
            addAnamnesis.Symptoms = anamnesis.Symptoms;
            _anamnesisService.Update(addAnamnesis);
            return Ok();
        }


        private bool CheckIfAppointmentIsDone(int appointmentId)
        {
            Anamnesis anamnesis = _anamnesisService.GetByAppointment(appointmentId);


            if (anamnesis == null)
            {
                return false;
            }
            return true;
        }

    }
}
