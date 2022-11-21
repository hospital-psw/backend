﻿namespace HospitalAPI.Dto.Auth
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
