namespace IntegrationAPI.Controllers
{
    using grpcServices;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using Microsoft.AspNetCore.Http;
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
        public IActionResult RequestBlood([FromBody] UrgentBloodTransfer request)
        {
            if (_service.RequestBlood(request))
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
