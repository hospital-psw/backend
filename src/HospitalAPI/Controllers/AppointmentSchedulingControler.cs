namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalAPI.Dto.SchedulingEvents;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.Examinations;
    using HospitalAPI.Mappers.Examinations.Events;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentSchedulingControler : BaseController<AppointmentSchedulingRoot>
    {
        private readonly IAppointmentSchedulingService _appointmentSchedulingService;
        public AppointmentSchedulingControler(IAppointmentSchedulingService service)
        {
            _appointmentSchedulingService = service;
        }

        //ENDPOINTI ZA SVAKU VRSTU DOGADJAJA

        [HttpPost("start")]
        public IActionResult StartSession(SessionStartedDto dto)
        {
            AppointmentSchedulingRoot root = _appointmentSchedulingService.StartSession(SchedulingEventMapper.SessionStartedDtoToEntity(dto));
            dto.AggregateId = root.Id;
            return Ok(root);
        }

        [HttpPost("next")]
        public IActionResult ClickNext(NextClickedDto dto)
        {
            return Ok(_appointmentSchedulingService.ClickNext(SchedulingEventMapper.NextDtoToEntity(dto)));
        }

        [HttpPost("back")]
        public IActionResult ClickBack(BackClickedDto dto)
        {
            return Ok(_appointmentSchedulingService.ClickBack(SchedulingEventMapper.BackDtoToEntity(dto)));
        }
        [HttpPost("date")]
        public IActionResult SelectDate(DateSelectedDto dto)
        {
            return Ok(_appointmentSchedulingService.SelectDate(SchedulingEventMapper.DateSelectedDtoToEntity(dto)));
        }
        [HttpPost("specialization")]
        public IActionResult SelectSpecialization(SpecializationSelectedDto dto)
        {
            return Ok(_appointmentSchedulingService.SelectSpecialization(SchedulingEventMapper.SpecializationSelectedDtoToEntity(dto)));
        }
        [HttpPost("doctor")]
        public IActionResult SelectDoctor(DoctorSelectedDto dto)
        {
            return Ok(_appointmentSchedulingService.SelectDoctor(SchedulingEventMapper.DoctorSelectedDtoToEntity(dto)));
        }
        [HttpPost("appointment")]
        public IActionResult SelectAppointment(AppointmentSelectedDto dto)
        {
            return Ok(_appointmentSchedulingService.SelectAppointment(SchedulingEventMapper.AppointmentSelectedDtoToEntity(dto)));
        }
        [HttpPost("schedule")]
        public IActionResult ScheduleAppointment(AppointmentScheduledDto dto)
        {
            return Ok(_appointmentSchedulingService.ScheduleAppointment(SchedulingEventMapper.AppointmentScheduledDtoToEntity(dto)));
        }
    }
}
