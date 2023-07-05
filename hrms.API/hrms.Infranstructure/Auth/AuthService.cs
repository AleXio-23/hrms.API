using hrms.Domain.Models.Auth;
using hrms.Persistance;
using hrms.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace hrms.Infranstructure.Auth
{
    public class AuthService : IAuthService
    {

        private readonly HrmsAppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthService(HrmsAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }


        public async Task<User> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (registerDto.Password != registerDto.RepeatPassword)
            {
                throw new ArgumentException("Passwords don't match");
            }
            if (await UserExists(registerDto.Username))
            {
                throw new ArgumentException("Username is registered");
            }

            var hmac = new HMACSHA512();
            var newUser = new User()
            {
                Username = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newUser;
        }

        public async Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                            .SingleOrDefaultAsync(x => x.Username == loginDto.Username.ToLower(), cancellationToken: cancellationToken);
            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt); 
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) throw new Exception("Incorrect password.");
            }

            return GenerateJwtToken(user);
        }
            

        public string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new System.Security.Claims.Claim("Id", user.Id.ToString()),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }


        private async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}
