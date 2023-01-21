namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAllergiesService _allergiesService;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, IAllergiesService allergiesService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _allergiesService = allergiesService;
        }

        public async Task<IdentityResult> Register(ApplicationUser user, string password)
        {
            var identityResult = await _userManager.CreateAsync(user, password);
            return identityResult;
        }

        public async Task<SignInResult> Login(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task SignInAsync(ApplicationUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationPatient> SetUpApplicationPatient(ApplicationPatient patient, int doctorId, List<int> allergies)
        {
            patient.applicationDoctor = (ApplicationDoctor)await _userManager.FindByIdAsync(doctorId.ToString());
            patient.Allergies = _allergiesService.GetAllergiesFromDTO(allergies);
            return patient;
        }

        public async Task<IdentityResult> RegisterPatient(ApplicationPatient patient, string password)
        {
            var identityResult = await _userManager.CreateAsync(patient, password);
            return identityResult;
        }

        public async Task<IdentityResult> AddToRole(ApplicationPatient patient, string name)
        {
            return await _userManager.AddToRoleAsync(patient, name);
        }

        public async Task<string> GetUserRole(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var rolename = await _userManager.GetRolesAsync(user);
            return rolename.FirstOrDefault();
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result;
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            var result = await _userManager.IsEmailConfirmedAsync(user);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
    }
}
