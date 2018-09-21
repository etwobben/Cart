using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using NSubstitute;
using Xunit;

namespace Domain.Test.Services
{
    public class BaseServiceTests : IClassFixture<BaseServiceTests>
    {
        private readonly IRepository<TestEntity> _repository;

        public BaseServiceTests()
        {
            _repository = Substitute.For<IRepository<TestEntity>>();

        }

        private TestService GetService()
        {
            return new TestService(_repository);
        }


        [Fact]
        public async void When_Service_GetAll_Expect_Repository_GetAll()
        {
            //Arrange
            var service = GetService();
            _repository.GetAllAsync().Returns(Task.FromResult(new List<TestEntity>() { new TestEntity() }));

            // Act
            var result = await service.GetAllAsync();

            //Assert
            await _repository.Received(1).GetAllAsync();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void When_Service_GetById_Expect_Repository_GetById()
        {
            //Arrange
            var expectedId = 1;
            var service = GetService();
            _repository.GetByIdAsync(Arg.Is(expectedId)).Returns(Task.FromResult(new TestEntity()));

            // Act
            var result = await service.GetByIdAsync(expectedId);

            //Assert
            await _repository.Received(1).GetByIdAsync(Arg.Is(expectedId));
            Assert.NotNull(result);
        }

        [Fact]
        public async void When_Service_Create_Expect_Repository_Create()
        {
            //Arrange
            var entity = new TestEntity();
            var service = GetService();
            _repository.InsertAsync(Arg.Is(entity)).Returns(Task.FromResult(entity));

            // Act
            var result = await service.InsertAsync(entity);

            //Assert
            await _repository.Received(1).InsertAsync(Arg.Is(entity));
            Assert.NotNull(result);
        }
    }

    public class TestService : BaseService<TestEntity>
    {
        public TestService(IRepository<TestEntity> repo) : base(repo)
        {

        }
    }

    public class TestEntity : BaseEntity
    {

    }
}
