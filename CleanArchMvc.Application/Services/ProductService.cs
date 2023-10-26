using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var command = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var query = new GetProductsQuery() ?? throw new ApplicationException($"Entity could not be loaded");
            var products = await _mediator.Send(query);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var query = new GetProductByIdQuery(id) ?? throw new ApplicationException($"Entity could not be loaded");
            var product = await _mediator.Send(query);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> GetByIdIncludeCategory(int? id)
        {
            var query = new GetProductByIdQuery(id) ?? throw new ApplicationException($"Entity could not be loaded");
            var product = await _mediator.Send(query);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task Remove(int? id)
        {
            var command = new ProductRemoveCommand(id) ?? throw new ApplicationException($"Entity could not be loaded");
            await _mediator.Send(command);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var command = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(command);
        }
    }
}
