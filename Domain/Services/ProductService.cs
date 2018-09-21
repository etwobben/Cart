using System;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> productRepository) : base(productRepository)
        {

        }
    }
}
