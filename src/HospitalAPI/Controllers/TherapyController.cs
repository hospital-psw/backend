namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class TherapyController : BaseController<Therapy>
    {

        private readonly ITherapyService _therapyService;

        public TherapyController(ITherapyService therapyService)
        {
            _therapyService = therapyService;
        }
    }
}
