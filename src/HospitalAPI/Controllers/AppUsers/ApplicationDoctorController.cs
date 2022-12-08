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
        public IActionResult GetBySpecialization(Specialization specialization) //ne znam ko ovo koristi i gde ali nemojte vracati celog doktora <3
        {
            var doctor = _doctorService.GetBySpecialization(specialization);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpGet("specializationDTO/{spec}")]
        public IActionResult GetBySpecializationDTO(Specialization spec)
        {
            var doctors = _doctorService.GetBySpecialization(spec);
            var DTOlist = new List<ApplicationDoctorDTO>();
            foreach(ApplicationDoctor doctor in doctors)
            {
                DTOlist.Add(ApplicationDoctorMapper.EntityToEntityDTO(doctor));
            }
            if (doctors == null)
                return NotFound();

            return Ok(DTOlist);
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
    }
}
