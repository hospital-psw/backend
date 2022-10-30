namespace IntegrationAPI.Controllers
{
    using AutoMapper;
    using IntegrationAPI.DTO;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.BloodBank.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodBankController : ControllerBase
    {
        private readonly IBloodBankService _bloodBankService;
        private readonly IMapper _mapper;

        public BloodBankController(IBloodBankService bloodBankService, IMapper mapper)
        {
            _bloodBankService = bloodBankService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<GetBloodBankDTO>>(_bloodBankService.GetAll()));
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            BloodBank entity = _bloodBankService.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetBloodBankDTO>(entity));
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterBloodBankDTO bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bloodBank == null)
            {
                return BadRequest();
            }

            BloodBank response = _bloodBankService.Create(_mapper.Map<BloodBank>(bloodBank));
            // renamme this method to register
            // generate API key and assign it to blood bank
            // generate dummy password assign it to blood bank
            // set dummy password
            // set IsDummyPassword flag to true
            // ALL OF THIS NEEDS TO BE HANDLED BY SERVICE

            return Ok(_mapper.Map<GetBloodBankDTO>(response));
        }

        [HttpPut]
        public IActionResult Update(UpdateBloodBankDTO bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var originalBloodBank = _bloodBankService.Get(bloodBank.Id);
            if (bloodBank == null || originalBloodBank == null)
            {
                return BadRequest();
            }

            var updatedBloodBank = _mapper.Map<BloodBank>(bloodBank);
            updatedBloodBank.ApiKey = originalBloodBank.ApiKey;
            updatedBloodBank.AdminPassword = originalBloodBank.AdminPassword;
            updatedBloodBank.IsDummyPassword = originalBloodBank.IsDummyPassword;

            var responseEntity = _bloodBankService.Update(updatedBloodBank);

            return Ok(responseEntity);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool response = _bloodBankService.Delete(id);
            if (!response)
            {
                return BadRequest(response);
            }

            return NoContent();
        }

    }
}
