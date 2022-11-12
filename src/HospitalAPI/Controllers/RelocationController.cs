namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RelocationController
    {
        private IRelocationService _relocationService;

        public RelocationController(IRelocationService relocationService)
        {
            _relocationService = relocationService;
        }
    }
}
