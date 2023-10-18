using Bogus;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new CategoryMockBuilder().WithValidValues().Build();
            action.Should().NotThrow<DomainExceptionValidation>();
        }
    }

    internal class CategoryMockBuilder
    {
        private readonly Faker<Category> _faker;

        public CategoryMockBuilder()
        {
            _faker = new Faker<Category>();
        }

        public Category Build()
        {
            return _faker.Generate();
        }

        public CategoryMockBuilder WithValidValues()
        {
            _faker
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());
            return this;
        }
    }
}