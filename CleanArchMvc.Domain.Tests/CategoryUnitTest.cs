using Bogus;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Tests.Mocks;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new CategoryMockBuilder().Build();
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-20)]
        [InlineData(-100)]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId(int id)
        {
            Action action = () => new CategoryMockBuilder().WithNegativeId(id).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value");
        }
    }
}