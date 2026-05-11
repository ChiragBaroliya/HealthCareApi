using HealthCare.Application.DTOs;
using HealthCare.Application.Models;
using HealthCare.Application.Services;
using HealthCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HealthCare.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HealthCareDbContext _db;

        public UserService(HealthCareDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return ResponseModel<LoginResponseDto>.Fail("Invalid credentials");

            // NOTE: PasswordHash is stored directly; for demo, compare plain text (replace with hash verification in production)
            if (user.PasswordHash != request.Password) return ResponseModel<LoginResponseDto>.Fail("Invalid credentials");

            // Generate JWT token
            var jwtSettings = new JwtSettings();
            // In real scenario, inject IConfiguration or options; for now hardcode defaults
            jwtSettings.Issuer = "HealthCareApi";
            jwtSettings.Audience = "HealthCareApiUsers";
            jwtSettings.Secret = "ThisIsASecretKeyForJwtTokenShouldBeStoredSafely";
            jwtSettings.ExpiryMinutes = 60;

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Username)
                }),
                Expires = System.DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = System.Guid.NewGuid().ToString();

            var response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = jwtSettings.ExpiryMinutes * 60
            };

            return ResponseModel<LoginResponseDto>.Ok(response);
        }
    }
}

