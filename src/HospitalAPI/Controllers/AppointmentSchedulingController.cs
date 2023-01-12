namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto.Examinations.Events;
    using HospitalAPI.Mappers.Examinations.Events;
    using HospitalAPI.Mappers.Examinations;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using HospitalAPI.Dto.SchedulingEvents;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using System.Collections.Generic;
    using System.Linq;
    using HospitalLibrary.Core.Model.Events.Scheduling;

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentSchedulingController : BaseController<AppointmentSchedulingRoot>
    {
        private readonly IAppointmentSchedulingService _appointmentSchedulingService;
        public AppointmentSchedulingController(IAppointmentSchedulingService service)
        {
            _appointmentSchedulingService = service;
        }

        //ENDPOINTI ZA SVAKU VRSTU DOGADJAJA

        [HttpPost("start")]
        public IActionResult StartSession(SessionStartedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.StartSession(SchedulingEventMapper.SessionStartedDtoToEntity(dto))));
        }

        [HttpPost("next")]
        public IActionResult ClickNext(NextClickedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.ClickNext(SchedulingEventMapper.NextDtoToEntity(dto))));
        }

        [HttpPost("back")]
        public IActionResult ClickBack(BackClickedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.ClickBack(SchedulingEventMapper.BackDtoToEntity(dto))));
        }
        [HttpPost("date")]
        public IActionResult SelectDate(DateSelectedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.SelectDate(SchedulingEventMapper.DateSelectedDtoToEntity(dto))));
        }
        [HttpPost("specialization")]
        public IActionResult SelectSpecialization(SpecializationSelectedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.SelectSpecialization(SchedulingEventMapper.SpecializationSelectedDtoToEntity(dto))));
        }
        [HttpPost("doctor")]
        public IActionResult SelectDoctor(DoctorSelectedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.SelectDoctor(SchedulingEventMapper.DoctorSelectedDtoToEntity(dto))));
        }
        [HttpPost("appointment")]
        public IActionResult SelectAppointment(AppointmentSelectedDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.SelectAppointment(SchedulingEventMapper.AppointmentSelectedDtoToEntity(dto))));
        }
        [HttpPost("schedule")]
        public IActionResult ScheduleAppointment(AppointmentScheduledDto dto)
        {
            return Ok(SchedulingEventMapper.EntityToDto(_appointmentSchedulingService.ScheduleAppointment(SchedulingEventMapper.AppointmentScheduledDtoToEntity(dto))));
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<SchedulingRootDto> appointmentsDto = new List<SchedulingRootDto>();
            List<AppointmentSchedulingRoot> appointments = _appointmentSchedulingService.GetAll().ToList();
            appointments.ForEach(a => appointmentsDto.Add(SchedulingEventMapper.EntityToDto(a)));
            return Ok(appointmentsDto);
        }

        [HttpGet("getAverageTimeSpent")]
        public IActionResult GetAverageTimeSpent()
        {
            return Ok(_appointmentSchedulingService.CalculateAverageTimeSpentToCreateAppointment());
        }
        [HttpGet("getAllS")]
        public IActionResult GetAllS()
        {
            List<SessionStarted> appointments = _appointmentSchedulingService.GetAllSessionStarted().ToList();
            
            return Ok(appointments);
        }
        [HttpGet("getAverageSteps")]
        public IActionResult GetAverageSteps()
        {
            return Ok(_appointmentSchedulingService.CalculateTheAverageNumberOfStepsToCreateAppointment());
        }


        [HttpGet("getAverageTimePerStep")]
        public IActionResult GetAverageTimePerStep()
        {
            return Ok(_appointmentSchedulingService.TimeSpentOnEachStep());
        }
 
        [HttpGet("getTimesOnSteps")]
        public IActionResult GetTimesOnSteps()

        {
            return Ok(_appointmentSchedulingService.CalculateNumberOfTimesSpentOnEachStep());
        }

        [HttpGet("getTimeToCreateAppointmentByAgeGroup")]
        public IActionResult AppointmentCreatingTimeByAgeGroup()

        {
            return Ok(_appointmentSchedulingService.CalculateAverageTimeSpentToCreateAppointmentForSpecificAgeGrouup());
        }
    }
}
