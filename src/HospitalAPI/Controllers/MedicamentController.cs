namespace HospitalAPI.Controllers
{
    using HospitalAPI.Mappers.Medicament;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

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
    }
}
