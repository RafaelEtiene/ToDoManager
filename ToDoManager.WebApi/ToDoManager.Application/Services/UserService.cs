using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Shared.Exceptions;

namespace ToDoManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> GetUserByNameAsync(string userName)
        {
            var user = await _userRepository.GetUserByNameAsync(userName);
            
            if(user == null)
            {
                throw new BusinessException($"The userName {userName} not found.");
            }

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task InsertUserAsync(InsertUserViewModel user)
        {
            await _userRepository.InsertUserAsync(_mapper.Map<User>(user));
        }
    }
}
