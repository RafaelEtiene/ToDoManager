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
using System.Diagnostics;

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
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, viewModel.Username.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
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
