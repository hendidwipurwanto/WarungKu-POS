using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Domain.DTOs;
using Warungku.Core.Domain.Entities;

namespace Warungku.Core.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request);
        Task<SignInResult> LoginAsync(LoginRequest request);
        Task<bool> UpdateUserAsync(UserRequest request);
        Task<bool> DeleteUserByIdAsync(string id);
        Task<UserRequest> GetUserById(string id);
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task LogoutAsync();
    }

}
