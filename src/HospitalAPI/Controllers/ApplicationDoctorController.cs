namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationDoctorController : BaseController<ApplicationDoctor>
    {

        private IApplicationDoctorService _applicationDoctorService;

        public ApplicationDoctorController(IApplicationDoctorService applicationDoctorService) : base()
        {
            _applicationDoctorService = applicationDoctorService;
        }
        [HttpGet("allrecommended")]
        public IActionResult GetAll()
        {
            List<ApplicationDoctorDto> applicationDoctorDto = new List<ApplicationDoctorDto>();
            List<ApplicationDoctor> applicationDoctors = _applicationDoctorService.RecommendDoctors().ToList();
            if (applicationDoctors == null)
            {
                return NotFound();
            }

            applicationDoctors.ForEach(bt => applicationDoctorDto.Add(ApplicationDoctorMapper.EntityToEntityDto(bt)));
            return Ok(applicationDoctorDto);
        }

    }


}

