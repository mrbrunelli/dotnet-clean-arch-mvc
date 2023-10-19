using Bogus;
using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests.Mocks
{
    public class CategoryMockBuilder
    {
        private readonly Faker<CategoryMockBuilder> _faker;
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;

        public CategoryMockBuilder()
        {
            _faker = new Faker<CategoryMockBuilder>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());
        }

        public Category Build()
        {
            var mock = _faker.Generate();
            return new Category(mock.Id, mock.Name);
        }

        public CategoryMockBuilder WithProperty<TProperty>(Expression<Func<CategoryMockBuilder, TProperty>> propertyExpression, TProperty value)
        {
            _faker.RuleFor(propertyExpression, f => value);
            return this;
        }
    }
}
