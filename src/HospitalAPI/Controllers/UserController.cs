namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private BaseService<User> _baseService = new BaseService<User>();

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public IActionResult GetAll() 
        {
            return Ok(_baseService.GetAll());
        }

    }
}
