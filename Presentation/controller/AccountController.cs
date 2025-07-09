using System.ComponentModel.DataAnnotations;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Account;
using FluentValidation;
using Presentation.Dto.Profile;
using ValidationException = FluentValidation.ValidationException;
using MediatR;
using Application.Commands.Account;
using Application.Queries.Account;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMapper mapper, IMediator mediator) : base(mapper)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _mediator.Send(new GetAllAccountsQuery());
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await _mediator.Send(new GetAccountByIdQuery(id));
            if (account == null)
                return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            var createdAccount = await _mediator.Send(new CreateAccountCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, createdAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAccountDto dto)
        {
            var updatedAccount = await _mediator.Send(new UpdateAccountCommand(id, dto));
            if (updatedAccount == null)
                return NotFound();
            return Ok(updatedAccount);
        }
    }
}