using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Account;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AuthController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var account = await _accountService.RegisterAsync(dto);
            return Ok(account);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _accountService.LoginAsync(dto);
            return Ok(response);
        }
    }
}