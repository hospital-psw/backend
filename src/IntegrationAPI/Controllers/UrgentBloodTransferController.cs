namespace IntegrationAPI.Controllers
{
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

        [HttpGet]
        public IActionResult Index()
        {
            _service.RequestBlood();

            return Ok();
        }
    }
}
