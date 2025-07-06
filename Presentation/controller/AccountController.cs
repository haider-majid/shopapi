using System.ComponentModel.DataAnnotations;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Account;
using FluentValidation;
using Presentation.Dto.Profile;
using ValidationException = FluentValidation.ValidationException;

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
            return await HandleServiceCall(
                () => _accountService.GetAllAsync(),
                accounts => HandleSuccess(accounts, "Accounts retrieved successfully")
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await HandleServiceCall(
                () => _accountService.GetByIdAsync(id),
                account => HandleSuccess(account, "Account retrieved successfully"),
                "Account not found"
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            return await HandleServiceCall(
                () => _accountService.CreateAsync(dto),
                createdAccount => HandleCreated(createdAccount, nameof(GetById), new { id = createdAccount.Id })
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAccountDto dto)
        {
            return await HandleServiceCall(
                () => _accountService.UpdateAsync(dto),
                account => HandleSuccess(account, "Account updated successfully"),
                "Account not found"
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await HandleServiceCall(
                () => _accountService.DeleteAsync(id),
                account => HandleSuccess(account, "Account deleted successfully"),
                "Account not found"
            );
        }
    }
}