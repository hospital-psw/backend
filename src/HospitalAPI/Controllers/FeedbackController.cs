namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
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

    }
}