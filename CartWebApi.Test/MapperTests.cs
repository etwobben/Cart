using System;
using AutoMapper;
using CartWebApi.AutoMapper;
using Domain.Entities;
using Models.ViewModels;
using Xunit;

namespace CartWebApi.Test
{
    public class MapperTests : IClassFixture<MapperTests>
    {
        private readonly IMapper _mapper;
        public MapperTests()
        {
            try
            {
                Mapper.Initialize(config =>
                {
                    config.AddProfile<ProductProfile>();
                    config.AddProfile<CartLineProfile>();

                });
            }
            catch (Exception)
            {
                //empty catch here because of random failure if Mapper is already initialized or not
            }

            _mapper = Mapper.Instance;
        }


        [Fact]
        public void When_Map_CartLine_To_ViewModel_Expect_Valid_Properties()
        {
            //Arrange
            var cartLine = new CartLine() { Amount = 5, Id = 1, Product = new Product() { Description = "description", Id = 15, Name = "name", Price = 19.95 } };

            //Act
            var viewModel = _mapper.Map<CartLineViewModel>(cartLine);

            //Assert
            Assert.Equal(cartLine.Id, viewModel.Id);
            Assert.Equal(cartLine.Amount, viewModel.Amount);
            Assert.Equal(cartLine.Product.Id, viewModel.Product.Id);
            Assert.Equal(cartLine.Product.Description, viewModel.Product.Description);
            Assert.Equal(cartLine.Product.Name, viewModel.Product.Name);
            Assert.Equal(cartLine.Product.Price, viewModel.Product.Price);
            Assert.Equal(cartLine.Product.Price * cartLine.Amount, viewModel.TotalPrice);
        }
    }
}
