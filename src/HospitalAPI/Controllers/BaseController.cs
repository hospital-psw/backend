namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
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

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
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