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
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new CategoryMockBuilder().WithNegativeId().Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value");
        }
    }
}