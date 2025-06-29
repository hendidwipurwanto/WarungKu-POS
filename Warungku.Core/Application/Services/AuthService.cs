using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;
using Warungku.Core.Domain.Entities;
using Warungku.Core.Infrastructure.Interfaces;

namespace Warungku.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IGenericRepository<User> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user);
            var response = _mapper.Map<AuthResponse>(user);
            response.Token = token;

            return response;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await UserExistsAsync(request.Username, request.Email))
                return null;

            var user = _mapper.Map<User>(request);
            user.PasswordHash = HashPassword(request.Password);

            var createdUser = await _userRepository.AddAsync(user);
            var token = GenerateJwtToken(createdUser);

            var response = _mapper.Map<AuthResponse>(createdUser);
            response.Token = token;

            return response;
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Username == username || u.Email == email);
            return user != null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
