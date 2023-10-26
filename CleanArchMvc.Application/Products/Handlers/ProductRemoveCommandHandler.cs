using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _repo;

        public ProductRemoveCommandHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(request.Id) ?? throw new ApplicationException($"Entity could not be found");

            return await _repo.RemoveAsync(product);
        }
    }
}
