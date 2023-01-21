namespace IntegrationAPI.Controllers
{
    using IntegrationAPI.DTO.UrgentBloodTransfer;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("report")]
        public IActionResult GenerateReport(GenerateReportDTO reportParams)
        {
            var report = _statisticsService.GenerateHTMLReport(reportParams.Start, reportParams.End, reportParams.SendEmail);
            report.Position = 0;
            return new FileStreamResult(report, "application/pdf");
        }
    }
}
