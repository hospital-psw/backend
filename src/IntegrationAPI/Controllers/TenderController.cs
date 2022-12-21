namespace IntegrationAPI.Controllers
{
    using AutoMapper;
    using IntegrationAPI.DTO.Tender;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

        [HttpGet("active")]
        public IActionResult GetActive()
        {
            return Ok(_mapper.Map<IEnumerable<GetTenderDTO>>(_tenderService.GetActive()));
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
        [HttpGet("finish/{tenderId}/{offerId}")]
        public IActionResult FinishTender(int tenderId, int offerId)
        {
            _tenderService.FinishTender(tenderId, offerId);
            return Ok(_mapper.Map<IEnumerable<GetTenderDTO>>(_tenderService.GetActive()));
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

        [HttpPut("MakeAnOffer/{tenderId}")]
        [Authorize]
        public IActionResult MakeAnOffer(int tenderId, MakeTenderOfferDTO tender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TenderOffer tenderOffer = new()
            {
                Offeror = new BloodBank()
                {
                    Id = int.Parse(User.FindFirst(x => x.Type == "Id")?.Value)
                },
                Items = tender.Items
            };

            TenderOffer validTenderOffer = _tenderService.MakeAnOffer(tenderId, tenderOffer);
            if (validTenderOffer == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<ViewTenderOfferDTO>(validTenderOffer));
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
        [HttpGet("money/{year}")]
        public IActionResult GethMonthMoneyStatistics(int year)
        {
            List<double> moneyPerMonth = _tenderService.GetMoneyPerMonth(year);
            if(moneyPerMonth == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(moneyPerMonth);
            }
        }
    }
}
