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
            try
            {
                var account = await _accountService.RegisterAsync(dto);
                return HandleCreated(account, nameof(Login), new { });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var response = await _accountService.LoginAsync(dto);
                return HandleSuccess(response, "Login successful");
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }
    }
}