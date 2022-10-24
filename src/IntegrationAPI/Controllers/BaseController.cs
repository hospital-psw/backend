namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.Core.Service;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        protected BaseService<TEntity> _baseService;

        public BaseController()
        {
            _baseService = new BaseService<TEntity>();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_baseService.GetAll());
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            TEntity entity = _baseService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Add(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (entity == null)
            {
                return BadRequest();
            }

            TEntity response = _baseService.Add(entity);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (entity == null)
            {
                return BadRequest();
            }

            TEntity ent = _baseService.Update(entity);

            return Ok(ent);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool response = _baseService.Delete(id);
            if (!response)
            {
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}
