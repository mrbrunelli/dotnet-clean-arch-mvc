using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _repo;

        public ProductUpdateCommandHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(request.Id) ?? throw new ApplicationException($"Entity could not be found");

            product.Update(
                request.Name,
                request.Description,
                request.Price,
                request.Stock,
                request.Image,
                request.CategoryId);

            return await _repo.UpdateAsync(product);
        }
    }
}
