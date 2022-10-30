﻿namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : BaseController<Feedback>
    {
        private IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService) : base()
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_feedbackService.GetAll());
        }

        [HttpGet("get/managerfeedback")]
        public IActionResult GetFeedbackForManager()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAll();
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.ForEach(f => managerFeedbackDto.Add(ManagerFeedbackMapper.EntityToEntityDto(f)));
            return Ok(managerFeedbackDto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Feedback feedback = _feedbackService.Get(id);
            return feedback is null ? NotFound() : Ok(feedback);
        }

        [HttpPost("add")]
        public IActionResult Add(NewFeedbackDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dto is null, please check your input.");
            }

            return Ok(_feedbackService.Add(dto));
        }

        [HttpPut("make/public/{id}")]
        public IActionResult MakePublic(int id)
        {
            bool status = _feedbackService.MakePublic(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

        [HttpPut("make/private/{id}")]
        public IActionResult MakePrivate(int id)
        {
            bool status = _feedbackService.MakePrivate(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

        [HttpPut("make/anonymous/{id}")]
        public IActionResult MakeAnonymous(int id)
        {
            bool status = _feedbackService.MakeAnonymous(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

        [HttpPut("make/identified/{id}")]
        public IActionResult MakeIdentified(int id)
        {
            bool status = _feedbackService.MakeIdentified(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

        [HttpGet("get/welcome/page")]
        public IActionResult GetWellcomePage()
        {
            return Ok(_feedbackService.GetForFrontPage());
        }

        [HttpPut("make/approved/{id}")]
        public IActionResult ApproveFeedback(int id)
        {
            bool status = _feedbackService.ApproveFeedback(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

        [HttpPut("make/denied/{id}")]
        public IActionResult DenyFeedback(int id)
        {
            bool status = _feedbackService.DenyFeedback(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

        [HttpPut("make/pending/{id}")]
        public IActionResult MakePending(int id)
        {
            bool status = _feedbackService.MakePending(id);
            return status is true ? Ok(status) : BadRequest("Something went wrong...");
        }

    }
}
