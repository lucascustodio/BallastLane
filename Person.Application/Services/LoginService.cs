using Microsoft.IdentityModel.Tokens;
using Person.Application.Services.Interfaces;
using Person.Domain.Aggregates.UserAggregate;
using Person.Domain.Aggregates.UserAggregate.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Person.Application.Services
{
    public class LoginService: ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.GetByEmail(username);
            
            if (user == null || !Domain.Core.Util.Crypto.VerifyPassword(password, user.Password!)) 
            {
                return null; 
            }

            var token = GenerateJwtToken(user);

            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Domain.Core.Util.Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
