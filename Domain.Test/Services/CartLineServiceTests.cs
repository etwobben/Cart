using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;
using NSubstitute;
using Xunit;

namespace Domain.Test.Services
{
    public class CartLineServiceTests : IClassFixture<CartLineServiceTests>
    {
        private readonly IProductService _productService;
        private readonly IRepository<CartLine> _cartLineRepository;


        public CartLineServiceTests()
        {
            _productService = Substitute.For<IProductService>();
            _cartLineRepository = Substitute.For<IRepository<CartLine>>();

        }

        private ICartLineService GetService()
        {
            return new CartLineService(_cartLineRepository, _productService);
        }

        [Fact]
        public async void When_UpdateCartLine_Expect_Updated_Amount()
        {
            //Arrange
            var service = GetService();
            var lineId = 1;
            _cartLineRepository.GetByIdAsync(lineId).Returns(Task.FromResult(new CartLine() { Id = 1, Amount = 2 }));

            //Act
            var cartLineViewModel = await service.UpdateCartLine(1, 5);

            //Assert
            await _cartLineRepository.Received(1).GetByIdAsync(1);
            Assert.Equal(5, cartLineViewModel.Amount);
            Assert.Equal(lineId, cartLineViewModel.Id);

        }

        [Fact]
        public async void When_UpdateCartLine_With_Invalid_Amount_Expect_Exception()
        {
            //Arrange
            var service = GetService();
            var lineId = 1;
            _cartLineRepository.GetByIdAsync(lineId).Returns(Task.FromResult(new CartLine() { Id = 1, Amount = 2 }));

            //Act
            var exception = await Record.ExceptionAsync(() => service.UpdateCartLine(1, -5));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async void When_UpdateCartLine_With_NonExisting_Expect_Exception()
        {
            //Arrange
            var service = GetService();
            _cartLineRepository.GetByIdAsync(1).Returns(Task.FromResult<CartLine>(null));

            //Act
            var exception = await Record.ExceptionAsync(() => service.UpdateCartLine(1, 5));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<EntityNotFoundException>(exception);
        }

        [Fact]
        public async void When_AddCartLine_With_Existing_ProductId_Expect_Updated_Amount()
        {
            //Arrange
            var service = GetService();
            _productService.GetByIdAsync(9).Returns(Task.FromResult(new Product()
            {
                Id = 9,
                Name = "test",
                Description = "test description",
                Price = 10
            }));
            var cartline = new CartLine() { Id = 1, ProductId = 9, Amount = 5 };
            _cartLineRepository.Where(Arg.Any<Expression<Func<CartLine, bool>>>()).Returns(Task.FromResult<List<CartLine>>(new List<CartLine>() { cartline }));

            //Act
            await service.AddCartLine(cartline.ProductId, 5);

            //Assert
            Assert.Equal(10, cartline.Amount);
        }
    }
}
