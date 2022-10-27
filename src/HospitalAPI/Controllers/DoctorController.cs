namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    //api/doctor/
    public class DoctorController : BaseController<Doctor>
    {
        public IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
    }
}
