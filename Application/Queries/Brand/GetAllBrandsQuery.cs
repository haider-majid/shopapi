using MediatR;
using Presentation.Dto.Brand;

namespace Application.Queries.Brand;

public record GetAllBrandsQuery : IRequest<IEnumerable<GetBrandDto>>;