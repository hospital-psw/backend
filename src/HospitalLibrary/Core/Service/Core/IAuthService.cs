namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<ApplicationPatient> SetUpApplicationPatient(ApplicationPatient patient, int doctorId, List<int> allergies);
        Task<IdentityResult> Register(ApplicationUser user, string password);
        Task<IdentityResult> RegisterPatient(ApplicationPatient patient, string password);
        Task<SignInResult> Login(string email, string password, bool rememberMe);
        Task SignInAsync(ApplicationUser user);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task SignOutAsync();
        Task<IdentityResult> AddToRole(ApplicationPatient patient, string role);
        Task<string> GetUserRole(int id);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
    }
}
