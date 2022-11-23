namespace HospitalAPI.Controllers
{
    using AutoMapper;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Auth;
    using HospitalAPI.TokenServices;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, IMapper mapper, ITokenService tokenService)
        {
            _authService = authService;
            _mapper = mapper;
            _tokenService = tokenService;
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
                    await _authService.AddToRole(identityUser, "Patient");
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _authService.Login(dto.Email, dto.Password, dto.RememberMe);

                if (loginResult.Succeeded)
                {
                    var user = await _authService.FindByEmailAsync(dto.Email);
                    var role = await _authService.GetUserRole(user.Id);
                    var token = _tokenService.BuildToken(user, role);
                    var result = _mapper.Map<LoginResponseDTO>(user);
                    result.Token = token;
                    return Ok(result);
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

        [Authorize]
        [HttpGet("kita")]
        public IActionResult Kita() 
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null || _tokenService.IsTokenValid(token)) 
            {
                return BadRequest("Mala kita");
            }

            return Ok("Velika kita");
        }

    }
}
