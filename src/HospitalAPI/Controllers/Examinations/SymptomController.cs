namespace HospitalAPI.Controllers.Examinations
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SymptomController : BaseController<Symptom>
    {
        private ISymptomService _symptomService;

        public SymptomController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_symptomService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Symptom symptom = _symptomService.Get(id);

            if(symptom == null)
            {
                return NotFound("Symptom doesnt exist");
            }

            return Ok(symptom);
        }

        [HttpPost]
        public IActionResult Add(Symptom symptom)
        {
            if (string.IsNullOrEmpty(symptom.Name))
            {
                return BadRequest("Name must not be empty");
            }


            Symptom existing = _symptomService.GetByName(symptom.Name);

            if (existing != null)
            {
                return BadRequest("Symptom already exists");
            }

            return Ok(_symptomService.Add(symptom));
        }
    }
}
