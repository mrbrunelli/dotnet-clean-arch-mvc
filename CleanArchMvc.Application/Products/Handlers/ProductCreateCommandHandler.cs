using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _repo;

        public ProductCreateCommandHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(
                request.Name,
                request.Description,
                request.Price,
                request.Stock,
                request.Image) ?? throw new ApplicationException($"Error creating entity");

            product.CategoryId = request.CategoryId;
            return await _repo.CreateAsync(product);
        }
    }
}
