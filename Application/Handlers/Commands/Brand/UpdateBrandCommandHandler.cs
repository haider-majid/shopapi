using MediatR;
using Application.Services;
using Application.Commands.Brand;

namespace Application.Handlers.Commands.Brand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IBrandService _brandService;

        public UpdateBrandCommandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.UpdateAsync(request.Id, request.Dto);
        }
    }
}
