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
        Task<IdentityResult> Register(ApplicationUser user, string password);
        Task<SignInResult> Login(string email, string password, bool rememberMe);
        Task SignInAsync(ApplicationUser user);
        Task<ApplicationUser> FindByEmailAsync(string email);
        
    }
}
