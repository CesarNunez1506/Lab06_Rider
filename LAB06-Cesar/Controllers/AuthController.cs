using LAB06_Cesar.Models;
using LAB06_Cesar.Repositories.Interface;
using LAB06_Cesar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB06_Cesar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly JwtService _jwtService;

        public AuthController(IAuthRepository authRepo, JwtService jwtService)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (await _authRepo.UserExists(registerDto.Username))
                return BadRequest("El nombre de usuario ya existe");

            var userToCreate = new Usuario
            {
                User = registerDto.Username,
                Role = registerDto.Role
            };

            var createdUser = await _authRepo.Register(userToCreate, registerDto.Password);

            return StatusCode(201, new { message = "Usuario registrado exitosamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginDto)
        {
            var user = await _authRepo.Login(loginDto.Username, loginDto.Password);

            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            var token = _jwtService.GenerateToken(user);

            return Ok(new UserResponseDTO
            {
                Id = user.IdUser,
                Username = user.User,
                Role = user.Role,
                Token = token
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdminData()
        {
            return Ok("Datos solo para administradores");
        }

        [Authorize(Roles = "User")]
        [HttpGet("user")]
        public IActionResult GetUserData()
        {
            return Ok("Datos solo para usuarios");
        }
    }
}
