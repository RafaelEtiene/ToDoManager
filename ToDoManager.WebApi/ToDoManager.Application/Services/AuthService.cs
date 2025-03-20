using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Shared.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BCrypt.Net;
using AutoMapper;

namespace ToDoManager.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository repository, IConfiguration configuration, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> GenerateJwtToken(LoginViewModel viewModel)
        {
            if (await GetUsers(viewModel.Username, viewModel.Password))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, viewModel.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(8),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new UnauthorizedAccessException("The password is incorrect.");
            }
        }

        public async Task RegisterAsync(LoginViewModel viewModel)
        {
            await _repository.InsertUserAsync(_mapper.Map<User>(viewModel));
        }

        private async Task<bool> GetUsers(string userName, string password)
        {
            var user = await _repository.GetUserByNameAsync(userName);

            if (user is null)
            {
                throw new UnauthorizedAccessException("The user is not registered in the API");
            }

            return VerifyPassword(password, user.PasswordHash);
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
