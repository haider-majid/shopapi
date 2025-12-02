using MediatR;
using Presentation.Dto.Brand;

namespace Application.Commands.Brand;

public record CreateBrandCommand(CreateBrandDto Dto) : IRequest<GetBrandDto>;