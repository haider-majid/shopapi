

using API.Controllers;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Profile;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService) : base(mapper)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            var account = await _accountService.CreateAsync(dto);
            return Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAccountDto dto)
        {
            var account = await _accountService.UpdateAsync(dto);
            return Ok(account);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var account = await _accountService.DeleteAsync(id);
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await _accountService.GetByIdAsync(id);
            return Ok(account);
        }
        
    }

}