namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class UrgentBloodTransferController : Controller
    {
        private readonly IUrgentBloodTransferService _service;
        private readonly IUrgentBloodTransferStatisticsService _statisticsService;

        public UrgentBloodTransferController(IUrgentBloodTransferService service, IUrgentBloodTransferStatisticsService statisticsService)
        {
            _service = service;
            _statisticsService = statisticsService;
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

        [HttpGet]
        public IActionResult GenerateReport()
        {
            return base.Content(_statisticsService.GenerateHTMLReport(new DateTime(2022, 12, 1), new DateTime(2022, 12, 31)), "text/html");
        }
    }
}
