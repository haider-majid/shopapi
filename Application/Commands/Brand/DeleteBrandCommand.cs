using MediatR;

namespace Application.Commands.Brand;

public record DeleteBrandCommand(Guid Id) : IRequest<bool>;