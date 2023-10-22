using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _repository.CreateAsync(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var category = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task Remove(int? id)
        {
            var category = await _repository.GetByIdAsync(id);
            // TODO create application exception
            await _repository.RemoveAsync(category);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _repository.UpdateAsync(category);
        }
    }
}
