using System.Net;
using System.Net.Http;
using System.Text;
using Models.DTOs;
using CartWebApi.Test.Fixtures;
using Domain.Entities;
using Newtonsoft.Json;
using Models.ViewModels;
using Xunit;

namespace CartWebApi.Test.Controllers
{
    [Collection("TestServer")]
    public class CartLineControllerTests : IClassFixture<CartLineControllerTests>
    {
        private readonly TestServerFixture _fixture;

        public CartLineControllerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void When_CartLine_Is_Added_Expect_Created_StatusCode_And_Added_Entity()
        {
            //Arrange
            var product = _fixture.GenerateProduct();
            var content = new StringContent(JsonConvert.SerializeObject(new AddCartLineDTO() { Amount = 10, ProductId = product.Id }), Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/cart", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<CartLineViewModel>(result);
            Assert.NotEqual(0, viewModel.Id);
            var entity = await _fixture.Context.CartLines.FindAsync(viewModel.Id);
            Assert.NotNull(entity);
            Assert.Equal(10, entity.Amount);
        }

        [Fact]
        public async void When_CartLine_Is_Updated_Expect_Ok_StatusCode_And_Updated_Amount()
        {
            //Arrange
            var newAmount = 10;

            var product = _fixture.GenerateProduct();
            var newCartLine = new CartLine() { ProductId = product.Id, Amount = 5 };
            await _fixture.Context.CartLines.AddAsync(newCartLine);
            await _fixture.Context.SaveChangesAsync();
            var content = new StringContent(JsonConvert.SerializeObject(new UpdateCartLineDTO() { LineId = newCartLine.Id, Amount = newAmount }), Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/cart/" + newCartLine.Id, content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<CartLineViewModel>(result);
            Assert.Equal(newCartLine.Id, viewModel.Id);
            Assert.Equal(newAmount, viewModel.Amount);

            await _fixture.Context.Entry(newCartLine).ReloadAsync(); //reload entity to refresh data
            ;
            Assert.Equal(newAmount, newCartLine.Amount);
        }

        [Fact]
        public async void When_Non_Existing_CartLine_Is_Updated_Expect_BadRequest_StatusCode()
        {
            //Arrange

            var content = new StringContent(JsonConvert.SerializeObject(new UpdateCartLineDTO() { LineId = 99999, Amount = 9 }), Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/cart/99999", content);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
