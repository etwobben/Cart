using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using Xunit;

namespace Domain.Test.Fixtures
{
    public class ProductRepositoryFixture
    {
        private readonly IRepository<Product> _productRepository;

        private readonly Product _testProduct;

        public ProductRepositoryFixture()
        {
            _productRepository = Substitute.For<IRepository<Product>>();

            _testProduct = new Product() { Id = 1, Name = "test", Description = "test description", Price = 10 };
            var products = new List<Product>() { _testProduct };

            _productRepository.GetAllAsync().Returns(Task.FromResult(products));
            _productRepository.GetByIdAsync(Arg.Any<int>()).Returns(id => Task.FromResult(products.Single(p => p.Id == id.Arg<int>())));
        }

        public IRepository<Product> GetRepository()
        {
            return _productRepository;
        }

        public Product GetTestProduct()
        {
            return _testProduct;

        }
    }

    [CollectionDefinition("ProductRepository")]
    public class ProductRepositoryCollection : ICollectionFixture<ProductRepositoryFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
