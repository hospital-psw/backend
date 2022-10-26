namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

    }
}
