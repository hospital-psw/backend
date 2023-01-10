namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Consilium;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.Consilium;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : BaseController<DoctorSchedule>
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        private readonly IAppointmentService _appointmentService;

        private readonly IConsiliumService _consiliumService;

        private readonly IVacationRequestsService _vacationRequestsService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService,
                                        IAppointmentService appointmentService,
                                        IConsiliumService consiliumService,
                                        IVacationRequestsService vacationRequestsService)
        {
            _doctorScheduleService = doctorScheduleService;
            _appointmentService = appointmentService;
            _consiliumService = consiliumService;
            _vacationRequestsService = vacationRequestsService;
        }

        [HttpGet("doctor/{id}")]
        public IActionResult GetDoctorSchedule(int id)
        {
            List<AppointmentDto> appointments = AppointmentMapper.EntityListToEntityDtoList(_appointmentService.GetByDoctorsId(id).ToList());
            List<DisplayConsiliumDto> consiliums = DisplayConsiliumMapper.EntityListToDtoList(_consiliumService.GetAllForDoctor(id));
            List<VacationRequestDto> vacations = VacationRequestsMapper.EntityListToDtoList(_vacationRequestsService.getAllApprovedByDoctorId(id).ToList());
            DoctorScheduleDto dto = new DoctorScheduleDto();
            dto.Appointments = appointments;
            dto.Consiliums = consiliums;
            dto.Vacations = vacations;
            return Ok(dto);
        }
    }
}
