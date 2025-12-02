using MediatR;
using Application.Services;
using Application.Commands.Brand;
using Presentation.Dto.Brand;

namespace Application.Handlers.Commands.Brand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, GetBrandDto>
{
    private readonly IBrandService _brandService;

    public CreateBrandCommandHandler(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<GetBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        return await _brandService.CreateAsync(request.Dto);
    }
}