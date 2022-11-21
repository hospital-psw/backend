namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class BloodExpenditureController : BaseController<BloodExpenditure>
    {


        private IBloodExpenditureService bloodExpenditureService;

        public BloodExpenditureController(IBloodExpenditureService _bloodExpenditureService)
        {

            bloodExpenditureService = _bloodExpenditureService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(bloodExpenditureService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(bloodExpenditureService.Get(id));
        }

        [HttpPost]
        public IActionResult Create(CreateExpenditureDTO createExpenditureDTO)
        {
            bloodExpenditureService.Create(createExpenditureDTO);
            return Ok();
        }

        [HttpPost("calculate")]
        public IActionResult CalculateExpenditure(DateRangeDto dto)
        {
            return Ok(bloodExpenditureService.CalculateExpenditure(dto.From, dto.To));
        }


    }
}
