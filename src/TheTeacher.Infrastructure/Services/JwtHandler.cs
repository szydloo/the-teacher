using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TheTeacher.Infrastructure.Dto;
using TheTeacher.Infrastructure.Settings;
using TheTeacher.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace TheTeacher.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        protected JwtHandler()
        {

        }

        public JwtDto CreateToken(Guid userId, string username, string role, bool isTeacher)
        {
            var now = DateTime.UtcNow;

            // Setting claims for token
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim("username", username),
                new Claim("isTeacher", isTeacher.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
            };

            var expires = now.AddMinutes(_jwtSettings.ExpiaryMinutes);
            // Signing credentials
            var signingCredentials = new SigningCredentials( new SymmetricSecurityKey((Encoding.UTF8.GetBytes(_jwtSettings.Key))), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimestamp()
            };
        }
    }
}