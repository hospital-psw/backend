namespace IntegrationAPI.Controllers
{
    using AutoMapper;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.News;
    using IntegrationAPI.DTO.Tender;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;


    [ApiController]
    [Route("api/[controller]")]
    public class TenderController : ControllerBase
    {
        private readonly ITenderService _tenderService;
        private readonly IMapper _mapper;

        public TenderController(ITenderService tenderService, IMapper mapper)
        {
            _tenderService = tenderService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<GetTenderDTO>>(_tenderService.GetAll()));
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            Tender entity = _tenderService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetTenderDTO>(entity));
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] CreateTenderDTO tender)
        {
            var entity = _tenderService.Create(_mapper.Map<Tender>(tender));

            if (entity is null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(UpdateTenderDTO tender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var originalTender = _tenderService.Get(tender.Id);
            if (tender == null || originalTender == null)
            {
                return BadRequest();
            }

            var responseEntity = _tenderService.Update(originalTender);

            return Ok(responseEntity);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool response = _tenderService.Delete(id);
            if (!response)
            {
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}
