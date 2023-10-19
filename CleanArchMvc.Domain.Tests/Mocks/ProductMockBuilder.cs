using Bogus;
using Bogus.DataSets;
using CleanArchMvc.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests.Mocks
{
    public class ProductMockBuilder
    {
        private readonly Faker<ProductMockBuilder> _faker;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

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

        public ProductMockBuilder WithProperty<TProperty>(Expression<Func<ProductMockBuilder, TProperty>> propertyExpression, TProperty value)
        {
            _faker.RuleFor(propertyExpression, f => value);
            return this;
        }
    }
}
