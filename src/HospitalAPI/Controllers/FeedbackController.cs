namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalAPI.TokenServices;
    using HospitalLibrary.Core.DTO.Feedback;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.AspNetCore.Authorization;
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
        private readonly ITokenService _tokenService;

        public FeedbackController(IFeedbackService feedbackService, ITokenService tokenService) : base()
        {
            _feedbackService = feedbackService;
            _tokenService = tokenService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_feedbackService.GetAll());
        }

        [HttpGet("get/manager/feedback")]
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

        [HttpGet("get/all/anonymous")]
        public IActionResult GetAllAnonymousFeedback()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAllAnonymousFeedback();
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.ForEach(f => managerFeedbackDto.Add(ManagerFeedbackMapper.EntityToEntityDto(f)));
            return Ok(managerFeedbackDto);
        }

        [HttpGet("get/all/approved")]
        public IActionResult GetAllAproved()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAllAproved();
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.ForEach(f => managerFeedbackDto.Add(ManagerFeedbackMapper.EntityToEntityDto(f)));
            return Ok(managerFeedbackDto);
        }

        [HttpGet("get/all/pending")]
        public IActionResult GetAllPendingFeedback()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAllPendingFeedback();
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.ForEach(f => managerFeedbackDto.Add(ManagerFeedbackMapper.EntityToEntityDto(f)));
            return Ok(managerFeedbackDto);
        }

        [HttpGet("get/all/denied")]
        public IActionResult GetAllDeniedFeedback()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAllDeniedFeedback();
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.ForEach(f => managerFeedbackDto.Add(ManagerFeedbackMapper.EntityToEntityDto(f)));
            return Ok(managerFeedbackDto);
        }

        [HttpGet("get/all/public")]
        public IActionResult GetAllPublicFeedback()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAllPublicFeedback();
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.ForEach(f => managerFeedbackDto.Add(ManagerFeedbackMapper.EntityToEntityDto(f)));
            return Ok(managerFeedbackDto);
        }

        [HttpGet("get/all/private")]
        public IActionResult GetAllPrivateFeedback()
        {
            List<ManagerFeedbackDto> managerFeedbackDto = new List<ManagerFeedbackDto>();
            List<Feedback> feedback = (List<Feedback>)_feedbackService.GetAllPrivateFeedback();
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

        [Authorize(Roles = "Patient")]
        [HttpPost("add")]
        public IActionResult Add(NewFeedbackDTO dto)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers["Authorization"];
                if (token == null || !_tokenService.IsTokenValid(token))
                {
                    return BadRequest("Invalid Authorization");
                }

                return Ok(_feedbackService.Add(dto));
            }
            else
            {
                return BadRequest("Something went wrong...");
            }
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
