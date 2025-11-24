using MediatR;
using Application.Services;
using Application.Commands.Brand;

namespace Application.Handlers.Commands.Brand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IBrandService _brandService;

        public DeleteBrandCommandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            return await _brandService.DeleteAsync(request.Id);
        }
    }
}
