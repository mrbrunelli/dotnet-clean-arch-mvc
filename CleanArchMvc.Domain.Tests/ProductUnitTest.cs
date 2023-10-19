using CleanArchMvc.Domain.Tests.Mocks;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest
    {
        private readonly ProductMockBuilder _productMock = new();

        [Fact]
        public void CreateProduct_ValidParameters_ResultValidObjectState()
        {
            Action action = () => _productMock.Build();
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_NullAndEmptyName_DomainExceptionInvalidName(string name)
        {
            Action action = () => _productMock.WithName(name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("12")]
        public void CreateProduct_ShortName_DomainExceptionInvalidName(string name)
        {
            Action action = () => _productMock.WithName(name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_NullAndEmptyDescription_DomainExceptionInvalidDescription(string description)
        {
            Action action = () => _productMock.WithDescription(description).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description. Description is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        [InlineData("1234")]
        public void CreateProduct_ShortDescription_DomainExceptionInvalidDescription(string description)
        {
            Action action = () => _productMock.WithDescription(description).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description. Too short, minimum 5 characters");
        }

        [Theory]
        [InlineData(-0.5)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void CreateProduct_NegativePrice_DomainExceptionInvalidPrice(decimal price)
        {
            Action action = () => _productMock.WithPrice(price).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void CreateProduct_NegativeStock_DomainExceptionInvalidStock(int stock)
        {
            Action action = () => _productMock.WithStock(stock).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock value");
        }

        [Theory]
        [InlineData(@"
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
            sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
            Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris 
            nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in 
            reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
            Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        )]
        public void CreateProduct_LongImageLocation_DomainExceptionInvalidImageLocation(string image)
        {
            Action action = () => _productMock.WithImage(image).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image location. Too long, maximum 250 characters");
        }
    }
}
