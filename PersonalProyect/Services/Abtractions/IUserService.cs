using Microsoft.AspNetCore.Identity;
using PersonalProyect.Data.Entities;
using PersonalProyect.Core;
using PersonalProyect.DTOs;

namespace PersonalProyect.Services.Abtractions
{
    public interface IUserService
    {
        public Task<Response<IdentityResult>> AddUserAsync(User user, string password);
        public Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token);
        public Task<Response<String>> GenerateConfirmationTokenAsync(User user);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<Response<SignInResult>> LoginAsync(LoginDTO dto);
        public Task<Response<IdentityResult>> SignupAsync(AccountUserDTO dto);
        public Task LogoutAsync();
        public Task<Response<AccountUserDTO>> UpdateUserAsync(AccountUserDTO dto);
        public bool CurrentUserIsAuthenticated();
        public Task<bool> CurrentUserHasPermissionAsync(string permission, string module);
    }
}
