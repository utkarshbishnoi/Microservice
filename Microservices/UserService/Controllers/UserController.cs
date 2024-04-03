using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Service;
using UserService.Repository;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly  UserRepository _userRepository;  // Add this line

        public UserController(UserRepository userRepository)  // Add UserRepository as a parameter
        {
            _userRepository = userRepository;  // Initialize userRepository
        }
        
        [HttpPost("userlogin")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            
            // Authenticate the user
            var user = await _userRepository.Login(loginRequest);
            if (user.Username == null)
            {
                return BadRequest(ModelState);
                //return Unauthorized("Invalid username or password");
            }
            //return Ok(user);
            return Ok(new
            {
                UserName = user.Username,
                Token = user.Token
            });
        }
    }
}
