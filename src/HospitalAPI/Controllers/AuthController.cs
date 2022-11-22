namespace HospitalAPI.Controllers
{
    using AutoMapper;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Auth;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService,IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto) 
        {
            if (ModelState.IsValid) 
            {
                var identityUser = _mapper.Map<ApplicationUser>(dto);
                var identityResult = await _authService.Register(identityUser, dto.Password);

                if (identityResult.Succeeded)
                {
                    //ovde treba odraditi confirm mail
                    await _authService.SignInAsync(identityUser);
                    return Ok(_mapper.Map<ApplicationUserDTO>(identityUser));
                }
                else 
                {
                    List<IdentityError> errorList = identityResult.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));
                    return BadRequest(errors);
                }
            }

            return BadRequest("Something went wrong...");
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient(RegisteredPatientDTO dto)
        {
            if (ModelState.IsValid)
            {
                var identityUser = await _authService.SetUpApplicationPatient(_mapper.Map<ApplicationPatient>(dto), dto.ChoosenDoctor, dto.Allergies);
                var identityResult = await _authService.RegisterPatient(identityUser, dto.ApplicationUserDTO.Password);

                if (identityResult.Succeeded)
                {
                    await _authService.AddToRole(identityUser);
                    await _authService.SignInAsync(identityUser);
                    return Ok(_mapper.Map<ApplicationUserDTO>(identityUser));
                }
                else
                {
                    List<IdentityError> errorList = identityResult.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));
                    return BadRequest(errors);
                }
            }

            return BadRequest("Something went wrong...");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _authService.Login(dto.Email, dto.Password, dto.RememberMe);

                if (loginResult.Succeeded)
                {
                    var user = await _authService.FindByEmailAsync(dto.Email);
                    return Ok(_mapper.Map<LoginResponseDTO>(user));
                }
                else
                {
                    return BadRequest("Invalid login!");
                }
            }

            return BadRequest("Something went wrong...");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return Ok("User logged out.");
        }

    }
}
