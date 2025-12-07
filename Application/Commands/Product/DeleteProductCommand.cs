using MediatR;

namespace Application.Commands.Product;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;
