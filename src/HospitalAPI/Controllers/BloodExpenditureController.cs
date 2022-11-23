namespace HospitalAPI.Controllers
{
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
        private IDoctorService doctorService;

        public BloodExpenditureController(IBloodExpenditureService _bloodExpenditureService, IDoctorService _doctorService)
        {

            bloodExpenditureService = _bloodExpenditureService;
            this.doctorService = _doctorService;
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
            Doctor doctor = doctorService.Get(createExpenditureDTO.DoctorId);
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


    }
}
