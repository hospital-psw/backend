namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : BaseController<Medicament>
    {

        private IMedicamentService _medicamentService;

        public MedicamentController(IMedicamentService medicamentService)
        {
            _medicamentService = medicamentService;
        }

    }
}
