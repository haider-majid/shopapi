using MediatR;
using Application.Services;
using Application.Queries.Brand;
using Presentation.Dto.Brand;

namespace Application.Handlers.Queries.Brand;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<GetBrandDto>>
{
    private readonly IBrandService _brandService;

    public GetAllBrandsQueryHandler(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<IEnumerable<GetBrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        return await _brandService.GetAllAsync();
    }
}
