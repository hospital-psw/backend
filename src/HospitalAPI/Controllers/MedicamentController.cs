namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.Medicament;
    using HospitalAPI.Mappers.Medicament;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : BaseController<Medicament>
    {

        private IMedicamentService _medicamentService;

        public MedicamentController(IMedicamentService medicamentService)
        {
            _medicamentService = medicamentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Medicament> medicaments = (List<Medicament>)_medicamentService.GetAll();
            return Ok(MedicamentMapper.EntityToEntityDtoList(medicaments));
        }

        [HttpGet("acceptable/{id}")]
        public IActionResult GetAcceptableMedicaments(int id)
        {
            List<Medicament> medicaments = _medicamentService.GetAcceptableMedicaments(id).ToList();
            List<MedicamentDto> dtoList = new List<MedicamentDto>();
            if (medicaments.IsNullOrEmpty())
            {
                return NotFound();
            }
            medicaments.ForEach(med => dtoList.Add(MedicamentMapper.EntityToEntityDto(med)));
            return Ok(dtoList);
        }
    }
}
