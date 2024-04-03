using Microsoft.AspNetCore.Identity.Data;
using UserService.Models;
using UserService.Service;

namespace UserService.Repository
{
    public class UserRepository
    {
        private List<User> _users = new List<User>
        {
        new User { Id = 1 ,Username = "admin", Password = "User@123", Role = "Admin" },
        new User { Id = 2 ,Username = "user", Password = "User@123", Role = "User" } 
        };

        private readonly TokenService _tokenService;

        public UserRepository(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Login (LoginDto loginDto)
        {
            var user =  _users.FirstOrDefault(x => x.Username.ToLower() == loginDto.Username.ToLower());
            bool isValid = user.Password.Equals(loginDto.Password); 
            
            var  token = _tokenService.GenerateToken(user, user.Role);
            
            LoginResponseDto response = new LoginResponseDto()
            {
                Username = user.Username,
                Token = token
            };
            return response;
        }
       
    }
}
