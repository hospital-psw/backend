namespace HospitalAPI.TokenServices
{
    using HospitalAPI.Configuration;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json;

    public class TokenService : ITokenService
    {
        private readonly ProjectConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(ProjectConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string BuildToken(ApplicationUser user, string role)
        {
            if (role == null)
                role = "Undefined";

            var issuer = _configuration.Jwt.Issuer;
            var audience = _configuration.Jwt.Audience;
            var key = Encoding.ASCII.GetBytes(_configuration.Jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, role)
                }),

                Expires = DateTime.UtcNow.AddMinutes(_configuration.Jwt.ExpiresIn),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public double GetExpireInDate()
        {
            return _configuration.Jwt.ExpiresIn;
        }

        public bool IsTokenValid(string token)
        {
            token = token.Replace("Bearer ", string.Empty);
            var mySecret = Encoding.UTF8.GetBytes(_configuration.Jwt.Key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration.Jwt.Issuer,
                    ValidAudience = _configuration.Jwt.Audience,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
