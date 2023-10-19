using Bogus;
using Bogus.DataSets;
using CleanArchMvc.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests.Mocks
{
    public class ProductMockBuilder
    {
        private readonly Faker<ProductMockBuilder> _faker;
        private int Id { get; set; }
        private string Name { get; set; } = string.Empty;
        private string Description { get; set; } = string.Empty;
        private decimal Price { get; set; } = decimal.Zero;
        private int Stock { get; set; }
        private string ImageUrl { get; set; } = string.Empty;

        public ProductMockBuilder()
        {
            _faker = new Faker<ProductMockBuilder>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(0.1m, 1000))
                .RuleFor(p => p.Stock, f => f.Random.Int(1, 300))
                .RuleFor(p => p.ImageUrl, f => f.Internet.Url());
        }

        public Product Build()
        {
            var mock = _faker.Generate();
            return new Product(
                mock.Id,
                mock.Name,
                mock.Description,
                mock.Price,
                mock.Stock,
                mock.ImageUrl
            );
        }

        public ProductMockBuilder WithName(string name)
        {
            _faker.RuleFor(p => p.Name, f => name);
            return this;
        }

        public ProductMockBuilder WithDescription(string description)
        {
            _faker.RuleFor(p => p.Description, f => description);
            return this;
        }

        public ProductMockBuilder WithPrice(decimal price)
        {
            _faker.RuleFor(p => p.Price, f => price);
            return this;
        }

        public ProductMockBuilder WithStock(int stock)
        {
            _faker.RuleFor(p => p.Stock, f => stock);
            return this;
        }

        public ProductMockBuilder WithImage(string imageUrl)
        {
            _faker.RuleFor(p => p.ImageUrl, f => imageUrl);
            return this;
        }
    }
}
