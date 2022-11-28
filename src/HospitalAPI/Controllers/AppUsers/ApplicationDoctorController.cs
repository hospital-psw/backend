namespace HospitalAPI.Controllers.AppUsers
{
    using AutoMapper;
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Model.Enums;
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
                                           IAuthService authService ,IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _authService = authService; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var doctor = _doctorService.Get(id);
            if(doctor == null)
                return NotFound();

            var result = _mapper.Map<ApplicationDoctorDTO>(doctor);
            result.Role = await _authService.GetUserRole(doctor.Id);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var doctors = _doctorService.GetAll().ToList();
            if (doctors == null)
                return NotFound();

            List<ApplicationDoctorDTO> doctorsDTO = new List<ApplicationDoctorDTO>();
            doctors.ForEach(d => doctorsDTO.Add(_mapper.Map<ApplicationDoctorDTO>(d)));
            return Ok(doctorsDTO);
        }

        [HttpGet("specialization/{spec}")]
        public IActionResult GetBySpecialization(Specialization specialization) 
        {
            //var doctor = _doctorService.GetBySpecialization(specialization);
            //if (doctor == null)
            //    return NotFound();

            //var result = _mapper.Map<ApplicationDoctorDTO>(doctor);
            return Ok(_doctorService.GetBySpecialization(specialization));
        }
    }
}
