using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _repository.CreateAsync(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> GetByIdIncludeCategory(int? id)
        {
            var product = await _repository.GetByIdIncludeCategoryAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task Remove(int? id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(product);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _repository.UpdateAsync(product);
        }
    }
}
