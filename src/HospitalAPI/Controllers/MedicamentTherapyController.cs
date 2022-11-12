namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.Therapy;
    using HospitalAPI.Mappers.Therapy;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentTherapyController : BaseController<MedicamentTherapy>
    {

        private readonly IMedicamentTherapyService _medicamentTherapyService;
        private readonly IMedicamentService _medicamentService;

        public MedicamentTherapyController(IMedicamentTherapyService medicamentTherapyService, IMedicamentService medicamentService)
        {
            _medicamentTherapyService = medicamentTherapyService;
            _medicamentService = medicamentService;
        }

        [HttpPost]
        public IActionResult Create(NewMedicamentTherapyDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            else if (dto.About == default(string) || dto.Amount == default(int) || dto.MedicamentId == default(int))
            {
                return BadRequest("Bad request, please enter valid data.");
            }

            Medicament medicament = _medicamentService.Get(dto.MedicamentId);

            if (medicament == null)
            {
                return NotFound();
            }

            MedicamentTherapyDto therapyDto = MedicamentTherapyMapper.EntityToEntityDto(_medicamentTherapyService.Add(NewMedicamentTherapyMapper.EntityDtoToEntity(dto, medicament), dto.MedicalTreatmentId));
            return Ok(therapyDto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            MedicamentTherapy medicamentTherapy = _medicamentTherapyService.Get(id);

            if (medicamentTherapy == null)
            {
                return NotFound();
            }

            return Ok(MedicamentTherapyMapper.EntityToEntityDto(medicamentTherapy));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<MedicamentTherapy> medicamentTherapies = _medicamentTherapyService.GetAll().ToList();
            List<MedicamentTherapyDto> dtoList = new List<MedicamentTherapyDto>();

            if (medicamentTherapies == null)
            {
                return NotFound();
            }

            medicamentTherapies.ForEach(mt => dtoList.Add(MedicamentTherapyMapper.EntityToEntityDto(mt)));
            return Ok(dtoList);
        }
    }
}
