namespace IntegrationAPI.Controllers
{
    using AutoMapper;
    using IntegrationAPI.DTO;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Exceptions;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodBankController : ControllerBase
    {
        private readonly IBloodBankService _bloodBankService;
        private readonly IMapper _mapper;
        private readonly IMailSender _mailer;

        public BloodBankController(IBloodBankService bloodBankService, IMapper mapper, IMailSender mailer)
        {
            _bloodBankService = bloodBankService;
            _mapper = mapper;
            _mailer = mailer;
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

        [HttpPost("register")]
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

            BloodBank response = _bloodBankService.Register(_mapper.Map<BloodBank>(bloodBank));

            return Ok(_mapper.Map<GetBloodBankDTO>(response));
        }

        [HttpPost("Login")]
        public IActionResult Login(BloodBankManagerLoginDTO credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (credentials == null)
                {
                    return BadRequest();
                }
                var bloodBank = _bloodBankService.GetByEmail(credentials.Email);
                if(bloodBank == null)
                {
                    return BadRequest();
                }
                if (bloodBank.AdminPassword.Equals(credentials.Password))
                {
                    return Ok(bloodBank.IsDummyPassword);
                }
                return Unauthorized();
            }catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (credentials == null)
                {
                    return BadRequest();
                }
                var bloodBank = _bloodBankService.GetByEmail(credentials.Email);
                if (!credentials.OldPassword.Equals(bloodBank.AdminPassword))
                {
                    return Unauthorized();
                }
                bloodBank.AdminPassword = credentials.NewPassword;
                bloodBank.IsDummyPassword = false;
                _bloodBankService.Update(bloodBank);
                return Ok(bloodBank);
            }catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
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

        [HttpGet("checkType/{id}/{type}")]
        public IActionResult CheckBloodType(int id, string type)
        {
            try
            {
                return Ok(_bloodBankService.CheckBloodType(id, type));
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }

        }

        [HttpGet("checkAmount/{id}/{type}/{amount}")]
        public IActionResult CheckBloodAmount(int id, string type, double amount)
        {
            try
            {
                return Ok(_bloodBankService.CheckBloodAmount(id, type, amount));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

    }
}
