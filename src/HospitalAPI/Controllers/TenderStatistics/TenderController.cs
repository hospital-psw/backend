namespace HospitalAPI.Controllers.TenderStatistics
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;


    [ApiController]
    [Route("api/[controller]")]
    public class TenderController : BaseController<Entity>
    {
        private readonly ITenderService _tenderService;

        public TenderController(ITenderService tenderService)
        {
            _tenderService = tenderService;
        }

        [HttpGet("money/{year}")]
        public IActionResult GethMonthMoneyStatistics(int year)
        {
            List<double> moneyPerMonth = _tenderService.GetMoneyPerMonth(year);
            if (moneyPerMonth == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(moneyPerMonth);
            }
        }
    }
}
