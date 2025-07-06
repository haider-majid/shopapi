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
            try
            {
                var accounts = await _accountService.GetAllAsync();
                return HandleSuccess(accounts, "Accounts retrieved successfully");
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var account = await _accountService.GetByIdAsync(id);
                if (account == null)
                    return HandleNotFoundException("Account not found");

                return HandleSuccess(account, "Account retrieved successfully");
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            try
            {
                var createdAccount = await _accountService.CreateAsync(dto);
                return HandleCreated(createdAccount, nameof(GetById), new { id = createdAccount.Id });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAccountDto dto)
        {
            try
            {
                var account = await _accountService.UpdateAsync(dto);
                if (account == null)
                    return HandleNotFoundException("Account not found");

                return HandleSuccess(account, "Account updated successfully");
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var account = await _accountService.DeleteAsync(id);
                if (account == null)
                    return HandleNotFoundException("Account not found");

                return HandleSuccess(account, "Account deleted successfully");
            }
            catch (Exception ex)
            {
                return HandleBadRequest(ex.Message);
            }
        }
    }
}