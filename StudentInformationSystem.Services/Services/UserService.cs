using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Services.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<ResponseDto> LoginAsync(string username, string password)
        {
            var user = await _repository.GetUserAsync(username);
            if (user is null)
                return new ResponseDto(false, "Username or password does not match");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new ResponseDto(false, "Username or password does not match");

            string token = CreateToken(user);
            return new ResponseDto(true, token);
        }

        public async Task<ResponseDto> SignupAsync(string username, string password)
        {
            var user = await _repository.GetUserAsync(username);
            if (user is not null)
                return new ResponseDto(false, "User already exists");

            user = await CreateUserAsync(username, password);
            await _repository.SaveUserAsync(user);
            return new ResponseDto(true);
        }

        private static async Task<User> CreateUserAsync(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                role = new User.Role()
            };
            return await Task.FromResult(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var secretToken = _configuration.GetSection("JWT:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

