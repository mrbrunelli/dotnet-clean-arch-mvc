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

        [Fact]
        public void CreateCategory_WithoutIdParameter_ResultObjectValidState()
        {
            Action action = () => new Category("Drinks");
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData("Beer Glass")]
        [InlineData("Premium Beer Glass")]
        public void CreateCategory_CompoundName_ResultObjectValidState(string name)
        {
            Action action = () => _categoryMock.WithProperty(p => p.Name, name);
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-20)]
        [InlineData(-100)]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId(int id)
        {
            Action action = () => _categoryMock.WithProperty(p => p.Id, id).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value");
        }

        [Theory]
        [InlineData("C")]
        [InlineData("Ca")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName(string name)
        {
            Action action = () => _categoryMock.WithProperty(p => p.Name, name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Name too short, minimum 3 characters");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CreateCategory_NullAndEmptyBlank_DomainExceptionRequiredName(string name)
        {
            Action action = () => _categoryMock.WithProperty(p => p.Name, name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
        }

        [Fact]
        public void UpdateCategory_NewValues_ResultObjectValidState()
        {
            var category = new Category("Drinks");
            var newName = "Alcoholic drinks";

            category.Update(newName);

            category.Name.Should().Be(newName);
        }
    }
}