using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Presentation.Dto.Brand;

namespace Application.Services;

public class BrandService : IBrandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetBrandDto>> GetAllAsync()
    {
        var brands = await _unitOfWork.BrandRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetBrandDto>>(brands);
    }

    public async Task<GetBrandDto?> GetByIdAsync(Guid id)
    {
        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
        return brand != null ? _mapper.Map<GetBrandDto>(brand) : null;
    }

    public async Task<GetBrandDto> CreateAsync(CreateBrandDto dto)
    {
        var brand = Brand.Create(dto.Name, dto.Description);
        var created = await _unitOfWork.BrandRepository.AddAsync(brand);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GetBrandDto>(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateBrandDto dto)
    {
        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
        if (brand == null)
            return false;

        brand.Update(dto.Name, dto.Description);
        
        await _unitOfWork.BrandRepository.UpdateAsync(brand);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
        if (brand == null)
            return false;

        await _unitOfWork.BrandRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}