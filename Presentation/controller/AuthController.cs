using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Presentation.Dto.Account;

namespace Presentation.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService, IMapper mapper) : base(mapper)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var account = await _accountService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Login), new { }, account);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _accountService.LoginAsync(dto);
            return Ok(response);
        }
    }
}