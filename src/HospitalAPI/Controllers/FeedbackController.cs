namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

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
        public override IActionResult GetAll() 
        {
            return Ok(_feedbackService.GetAll());
        }

        [HttpPost("create")]
        public IActionResult Create(NewFeedbackDTO dto)
        {
            if (dto == null) 
            {
                return BadRequest("Dto is null.");
            }

            return Ok(_feedbackService.Create(dto));
        }
    }
}
