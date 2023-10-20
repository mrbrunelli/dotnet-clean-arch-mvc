using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Tests.Mocks;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

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

        [Fact]
        public void CreateProduct_WithoutId_ResultValidObjectState()
        {
            Action action = () => new Product("Beer", "Coldest beer", 5.99m, 350, "/image-path.png");
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-1000)]
        public void CreateProduct_NegativeId_DomainExceptionInvalidId(int id)
        {
            Action action = () => _productMock.WithProperty(p => p.Id, id).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_NullAndEmptyName_DomainExceptionInvalidName(string name)
        {
            Action action = () => _productMock.WithProperty(p => p.Name, name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Name is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("12")]
        public void CreateProduct_ShortName_DomainExceptionInvalidName(string name)
        {
            Action action = () => _productMock.WithProperty(p => p.Name, name).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. Too short, minimum 3 characters");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_NullAndEmptyDescription_DomainExceptionInvalidDescription(string description)
        {
            Action action = () => _productMock.WithProperty(p => p.Description, description).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description. Description is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        [InlineData("1234")]
        public void CreateProduct_ShortDescription_DomainExceptionInvalidDescription(string description)
        {
            Action action = () => _productMock.WithProperty(p => p.Description, description).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description. Too short, minimum 5 characters");
        }

        [Theory]
        [InlineData(-0.5)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void CreateProduct_NegativePrice_DomainExceptionInvalidPrice(decimal price)
        {
            Action action = () => _productMock.WithProperty(p => p.Price, price).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void CreateProduct_NegativeStock_DomainExceptionInvalidStock(int stock)
        {
            Action action = () => _productMock.WithProperty(p => p.Stock, stock).Build();
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
            Action action = () => _productMock.WithProperty(p => p.ImageUrl, image).Build();
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image location. Too long, maximum 250 characters");
        }

        [Fact]
        public void CreateProduct_NullImagePath_NoNullReferenceException()
        {
            Action action = () => _productMock.WithProperty(p => p.ImageUrl, null).Build();
            action.Should().NotThrow<NullReferenceException>();
        }

        [Fact]
        public void UpdateProduct_NewValues_ResultValidObjectState()
        {
            var product = new Product("Ber", "Warmest beer", 5.99m, 350, "/image-path.png");
            var newName = "Beer";
            var newDesc = "Coldest beer";
            var newPric = 7.20m;
            var newStoc = 210;
            var newImag = "/img-path.jpg";
            var newCaId = 4;

            product.Update(newName, newDesc, newPric, newStoc, newImag, newCaId);

            product.Name.Should().Be(newName);
            product.Description.Should().Be(newDesc);
            product.Price.Should().Be(newPric);
            product.Stock.Should().Be(newStoc);
            product.Image.Should().Be(newImag); 
            product.CategoryId.Should().Be(newCaId);
        }
    }
}
