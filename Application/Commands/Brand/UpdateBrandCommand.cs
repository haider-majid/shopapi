using MediatR;
using Presentation.Dto.Brand;

namespace Application.Commands.Brand;

public record UpdateBrandCommand(Guid Id, UpdateBrandDto Dto) : IRequest<bool>;
