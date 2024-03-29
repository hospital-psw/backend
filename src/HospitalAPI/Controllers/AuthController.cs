﻿namespace HospitalAPI.Controllers
{
    using AutoMapper;
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPI.Dto.Auth;
    using HospitalAPI.EmailServices;
    using HospitalAPI.TokenServices;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Net.Http.Headers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IMapper mapper,
            ITokenService tokenService, IEmailService emailService)
        {
            _authService = authService;
            _mapper = mapper;
            _tokenService = tokenService;
            _emailService = emailService;
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
                    var token = await _authService.GenerateEmailConfirmationTokenAsync(identityUser);
                    var param = new Dictionary<string, string?>
                    {
                        {"token", token },
                        {"email", identityUser.Email }
                    };

                    //izgenerisati email poruku i poslati je putem _emailServic-a;

                    var result = _mapper.Map<ApplicationUserDTO>(identityUser);
                    return Ok(result);
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
                    var token = await _authService.GenerateEmailConfirmationTokenAsync(identityUser);

                    await _emailService.SendActivationEmail(identityUser.Email, token);
                    var result = _mapper.Map<ApplicationUserDTO>(identityUser);
                    return Ok(result);
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
                    var token = await _tokenService.BuildToken(user, role);
                    var result = _mapper.Map<LoginResponseDTO>(user);
                    result.Token = token;
                    result.ExpiresIn = _tokenService.GetExpireInDate();
                    return Ok(result);
                }
                else if (loginResult.IsNotAllowed)
                {
                    return BadRequest("Not allowed, please verify your account.");
                }
                else
                {
                    return BadRequest("Invalid credentials.");
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

        [HttpPost("reset/password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _authService.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("There is no user with entered email.");
            var result = await _authService.ResetPasswordAsync(user, dto.Token, dto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok(JsonSerializer.Serialize("Your password has been successfully changed."));
        }

        [HttpPost("forgot/password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Request");
            var user = await _authService.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("No user with entered email found");
            var token = await _authService.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendPasswordResetEmail(dto.Email, dto.ClientURI, token);

            return Ok(JsonSerializer.Serialize("Email has been sent."));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _authService.FindByEmailAsync(email);
            var confirmResult = await _authService.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
            {
                return BadRequest("Invalid Email Confirmation Request");
            }
            return Redirect("http://localhost:4200/login");
        }

    }
}
