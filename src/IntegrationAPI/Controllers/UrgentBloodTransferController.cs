namespace IntegrationAPI.Controllers
{
    using grpcServices;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UrgentBloodTransferController : Controller
    {
        private readonly IUrgentBloodTransferService _service;

        public UrgentBloodTransferController(IUrgentBloodTransferService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult RequestBlood([FromBody] UrgentBloodTransferRequest request)
        {
            if(_service.RequestBlood(request))
                return Ok();
            else
                return BadRequest();
        }
    }
}
