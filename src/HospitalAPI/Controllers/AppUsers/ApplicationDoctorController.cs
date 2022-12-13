namespace HospitalAPI.Controllers.AppUsers
{
    using AutoMapper;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.AppUsers;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.AppUsers;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]")]
    public class ApplicationDoctorController : ControllerBase
    {
        private readonly IApplicationDoctorService _doctorService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public ApplicationDoctorController(IApplicationDoctorService doctorService,

                                           IAuthService authService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = _doctorService.Get(id);
            if (doctor == null)
                return NotFound();

            var result = _mapper.Map<ApplicationDoctorDTO>(doctor);
            result.Role = await _authService.GetUserRole(doctor.Id);
            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var doctors = _doctorService.GetAll().ToList();
            if (doctors == null)
                return NotFound();

            List<ApplicationDoctorDTO> doctorsDTO = new List<ApplicationDoctorDTO>();
            doctors.ForEach(d => doctorsDTO.Add(ApplicationDoctorMapper.EntityToEntityDTO(d)));
            return Ok(doctorsDTO);
        }

        [HttpGet("specialization/{spec}")]
        public IActionResult GetBySpecialization(Specialization specialization)
        {
            var doctor = _doctorService.GetBySpecialization(specialization);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpGet("allrecommended")]
        public IActionResult GetAllRecomended()
        {
            List<ApplicationDoctorDTO> applicationDoctorDto = new List<ApplicationDoctorDTO>();
            List<ApplicationDoctor> applicationDoctors = _doctorService.RecommendDoctors().ToList();
            if (applicationDoctors == null)
            {
                return NotFound();
            }

            applicationDoctors.ForEach(bt => applicationDoctorDto.Add(ApplicationDoctorMapper.EntityToEntityDTO(bt)));
            return Ok(applicationDoctorDto);
        }

        [HttpGet("same-shift/{workHourId}")]
        public IActionResult GetDoctorsWhoWorksInSameShift(int workHourId)
        {
            List<ApplicationDoctor> doctors = _doctorService.GetDoctorsWhoWorksInSameShift(workHourId).ToList();
            List<ApplicationDoctorDTO> dtoList = new List<ApplicationDoctorDTO>();
            if (doctors == null)
            {
                return NotFound();
            }
            doctors.ForEach(doc => dtoList.Add(ApplicationDoctorMapper.EntityToEntityDTO(doc)));
            return Ok(dtoList);
        }

        [HttpGet("specializations/{workHourId}")]
        public IActionResult GetSpecializationsOfDoctorsWhoWorksInSameShift(int workHourId)
        {
            List<Specialization> specializations = _doctorService.GetSpecializationsOfDoctorsWhoWorksInSameShift(workHourId).ToList();
            if (specializations.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(specializations);
        }
    }
}
