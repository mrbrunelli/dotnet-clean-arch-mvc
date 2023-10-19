using Bogus;
using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests.Mocks
{
    public class CategoryMockBuilder
    {
        private readonly Faker<Category> _faker;

        public CategoryMockBuilder()
        {
            _faker = new Faker<Category>()
                .CustomInstantiator(f => new Category(f.IndexFaker, f.Commerce.Categories(1).First()));
        }

        public Category Build()
        {
            return _faker.Generate();
        }

        public CategoryMockBuilder WithNegativeId(int id)
        {
            _faker
                .CustomInstantiator(f => new Category(id, f.Commerce.Categories(1).First()));
            return this;
        }

        public CategoryMockBuilder WithInvalidName(string name)
        {
            _faker
                .CustomInstantiator(f => new Category(f.IndexFaker, name));
            return this;
        }
    }
}
