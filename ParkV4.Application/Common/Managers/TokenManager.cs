using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Auth.Queries.Login.Dtos;
using System.Text;

namespace ParkV4.Application.Common.Managers
{
    public class TokenManager
    {

        private readonly TokenSetting _tokenSetting;
        private readonly IApplicationContext _context;

        public TokenManager(IOptions<TokenSetting> tokenSetting, IApplicationContext context)
        {
            _tokenSetting = tokenSetting.Value;
            _context = context;
        }
        
        public async Task<LoginDto> GenerateToken(User appUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.Username),
                new Claim(ClaimTypes.GivenName, appUser.FullName),
                new Claim(ClaimTypes.Role, "Normal")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.Key));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime tokenExpire = DateTime.Now.AddHours(_tokenSetting.TokenValidityTime);

            JwtSecurityToken token = new JwtSecurityToken(
                _tokenSetting.Issuer,
                _tokenSetting.Audience,
                claims,
                expires: tokenExpire,
                signingCredentials: credentials
            );

            DateTime refreshTokenExpire = DateTime.Now.AddMinutes(_tokenSetting.RefreshTokenValidityTime);
            var refreshToken = CreateRefreshToken();

            LoginDto summary = new LoginDto
            {
                Email = appUser.Email,
                FirstName = appUser.Name,
                LastName = appUser.Surname,
                Role = "Normal",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                RefreshTokenExpireTime = refreshTokenExpire,
                TokenExpireTime = tokenExpire
            };

            appUser.Token = summary.Token;
            appUser.RefreshToken = summary.RefreshToken;
            appUser.TokenExpireTime = summary.TokenExpireTime;
            appUser.RefreshTokenExpireTime = summary.RefreshTokenExpireTime;

            _context.Users.Update(appUser);
            await _context.SaveChangesAsync(CancellationToken.None);
            
            return summary;
        }

        private string CreateRefreshToken()
        {
            byte[] bytes = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
                return Guid.NewGuid().ToString("N") + string.Concat(bytes.Select(x => x.ToString("x2")));
            }
        }
    }
}