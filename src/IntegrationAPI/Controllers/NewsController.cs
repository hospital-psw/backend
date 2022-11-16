namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.News;
    using AutoMapper;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.News;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.News.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ManagerNewsDTO>>(_newsService.GetAll()));
        }

        [HttpGet("published")]
        public virtual IActionResult GetPublished()
        {
            return Ok(_mapper.Map<IEnumerable<ManagerNewsDTO>>(_newsService.GetPublished()));
        }

        [HttpGet("archived")]
        public virtual IActionResult GetArchived()
        {
            return Ok(_mapper.Map<IEnumerable<ManagerNewsDTO>>(_newsService.GetArchived()));
        }

        [HttpGet("pending")]
        public virtual IActionResult GetPending()
        {
            return Ok(_mapper.Map<IEnumerable<ManagerNewsDTO>>(_newsService.GetPending()));
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var entity = _newsService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ManagerNewsDTO>(entity));
        }

        [HttpPost("archive/{id}")]
        public virtual IActionResult Archive(int id)
        {
            if (_newsService.Archive(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("publish/{id}")]
        public virtual IActionResult Publish(int id)
        {
            if (_newsService.Publish(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody]UserNewsDTO news)
        {
            var entity = _newsService.Create(_mapper.Map<News>(news));

            if (entity is null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
