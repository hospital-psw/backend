namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.News.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("published")]
        public virtual IActionResult GetPublished()
        {
            throw new NotImplementedException();
        }

        [HttpGet("archived")]
        public virtual IActionResult GetArchived()
        {
            throw new NotImplementedException();
        }

        [HttpGet("pending")]
        public virtual IActionResult GetPending()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("archive/{id}")]
        public virtual IActionResult Archive(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("publish/{id}")]
        public virtual IActionResult Publish(int id)
        {
            throw new NotImplementedException();
        }
    }
}
