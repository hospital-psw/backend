namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : BaseController<Doctor>
    {
        public IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public IActionResult Add(NewDoctorDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            else if (dto.Email == default(string) || dto.FirstName == default(string) || dto.LastName == default(string) || dto.Password == default(string))
            {
                return BadRequest("Bad request, please enter valid data.");
            }

            return Ok(_doctorService.Add(NewDoctorMapper.EntityDtoToEntity(dto)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Doctor doctor = _doctorService.Get(id);
            if (doctor == null)
            {
                return NotFound();
            }
            DoctorDto dto = DoctorMapper.EntityToEntityDto(doctor);
            return Ok(dto);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<DoctorDto> doctorsDto = new List<DoctorDto>();
            List<Doctor> doctors = _doctorService.GetAll().ToList();
            if (doctors == null)
            {
                return NotFound();
            }
            doctors.ForEach(doctor => doctorsDto.Add(DoctorMapper.EntityToEntityDto(doctor)));
            return Ok(doctorsDto);
        }

        [HttpPut]
        public IActionResult Update(UpdateDoctorDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            else if (dto.Email == default(string) || dto.FirstName == default(string) || dto.LastName == default(string) || dto.Id == default(int))
            {
                return BadRequest("Bad request, please enter valid data.");
            }


            Doctor doctor = _doctorService.Get(dto.Id);
            if (doctor == null || doctor.Deleted)
            {
                return NotFound();
            }

            return Ok(_doctorService.Update(UpdateDoctorMapper.EntityDtoToEntity(dto)));
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(int doctorId)
        {
            Doctor doctor = _doctorService.Get(doctorId);
            if (doctor == null || doctor.Deleted)
            {
                return NotFound();
            }

            bool response = _doctorService.Delete(doctorId);
            if (!response)
            {
                return BadRequest(response);
            }

            return NoContent();
        }
    }
}
