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
            return await HandleServiceCall(
                () => _accountService.RegisterAsync(dto),
                account => HandleCreated(account, nameof(Login), new { })
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            return await HandleServiceCall(
                () => _accountService.LoginAsync(dto),
                response => HandleSuccess(response, "Login successful")
            );
        }
    }
}