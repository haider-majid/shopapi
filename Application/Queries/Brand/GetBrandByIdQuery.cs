using MediatR;
using Presentation.Dto.Brand;

namespace Application.Queries.Brand;

public record GetBrandByIdQuery(Guid Id) : IRequest<GetBrandDto?>;