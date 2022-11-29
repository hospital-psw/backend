namespace HospitalAPI.Controllers.AppUsers
{
    using AutoMapper;
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]")]
    public class ApplicationPatientController : ControllerBase
    {
        private readonly IApplicationPatientService _appPatientService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public ApplicationPatientController(IApplicationPatientService appPatientService, 
                                            IAuthService authService, IMapper mapper)
        {
            _appPatientService = appPatientService;
            _mapper = mapper;
            _authService = authService;
        }

        public ApplicationPatientController(IApplicationPatientService appPatientService)
        {
            _appPatientService = appPatientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var patient = _appPatientService.Get(id);
            if (patient == null)
                return NotFound();

            var result = _mapper.Map<ApplicationPatientDTO>(patient);
            result.Role = await _authService.GetUserRole(id);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() 
        {
            List<ApplicationPatientDTO> patientsDTO = new List<ApplicationPatientDTO>();
            var patients = _appPatientService.GetAll().ToList();
            if (patients == null)
                return NotFound();

            patients.ForEach(p => patientsDTO.Add(_mapper.Map<ApplicationPatientDTO>(p)));
            return Ok(patientsDTO);
        }

        [HttpGet("non-hospitalized")]
        public IActionResult GetNonHospitalized() 
        {
            List<ApplicationPatientDTO> patientDTOs = new List<ApplicationPatientDTO>();
            var patients = _appPatientService.GetNonHospitalized().ToList();
            if (patients == null)
                return NotFound();

            patients.ForEach(p => patientDTOs.Add(_mapper.Map<ApplicationPatientDTO>(p)));
            return Ok(patientDTOs);
        }
    }
}
