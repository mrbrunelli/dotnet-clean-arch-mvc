using Bogus;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Tests.Mocks;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest
    {
        private readonly CategoryMockBuilder _categoryMock = new();

        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => _categoryMock.Build();
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-20)]
        [InlineData(-100)]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId(int id)
        {
            Action action = () => _categoryMock.WithNegativeId(id).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value");
        }

        [Theory]
        [InlineData("C")]
        [InlineData("Ca")]
        public void CreateCategory_ShortNameValue_DomainExceptionInvalidName(string name)
        {
            Action action = () => _categoryMock.WithInvalidName(name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Name too short, minimum 3 characters");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void CreateCategory_MissingNameValue_DomainExceptionInvalidName(string name)
        {
            Action action = () => _categoryMock.WithInvalidName(name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
        }
    }
}