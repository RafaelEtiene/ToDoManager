using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;

namespace ToDoManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(LoginViewModel viewModel);
        Task RegisterAsync(LoginViewModel viewModel);
    }
}
