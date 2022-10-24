namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : BaseController<Appointment>
    {
        private IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService    appointmentService){
            _appointmentService   =    appointmentService;
        }
    }
}