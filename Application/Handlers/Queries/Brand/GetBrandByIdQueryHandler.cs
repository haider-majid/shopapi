using MediatR;
using Application.Services;
using Application.Queries.Brand;
using Presentation.Dto.Brand;

namespace Application.Handlers.Queries.Brand;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, GetBrandDto?>
{
    private readonly IBrandService _brandService;

    public GetBrandByIdQueryHandler(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<GetBrandDto?> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        return await _brandService.GetByIdAsync(request.Id);
    }
}