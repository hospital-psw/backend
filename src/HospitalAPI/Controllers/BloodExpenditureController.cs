namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class BloodExpenditureController : BaseController<BloodExpenditure>
    {
        private IBloodExpenditureService bloodExpenditureService;
        private readonly IApplicationDoctorService _doctorService;

        public BloodExpenditureController(IBloodExpenditureService _bloodExpenditureService, IApplicationDoctorService doctorService)
        {

            bloodExpenditureService = _bloodExpenditureService;
            _doctorService = doctorService;
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
            ApplicationDoctor doctor = _doctorService.Get(createExpenditureDTO.DoctorId);
            if (createExpenditureDTO == null)
            {
                return BadRequest("DTO is null");
            }
            if (createExpenditureDTO.Reason == null || createExpenditureDTO.Amount < 0 || createExpenditureDTO.BloodType < 0)
            {
                return BadRequest("Incorrect data");
            }
            if (doctor == null)
            {
                return BadRequest("Doctor does not exist");
            }

            bloodExpenditureService.Create(createExpenditureDTO);
            return Ok(createExpenditureDTO);
        }

        [HttpPost("calculate")]
        public IActionResult CalculateExpenditure(DateRangeDto dto)
        {
            return Ok(bloodExpenditureService.CalculateExpenditure(dto.From, dto.To));
        }


    }
}
