﻿namespace HospitalLibrary.Core.Service
{
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

        public AuthService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

    }
}