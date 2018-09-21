using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Domain.Services
{
    public class CartLineService : BaseService<CartLine>, ICartLineService
    {
        private readonly IRepository<Product> _productRepository;

        public CartLineService(IRepository<CartLine> cartlineRepository, IRepository<Product> productRepository) : base(cartlineRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CartLine> UpdateCartLine(int lineId, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount should be higher than 0");

            var line = await Repository.GetByIdAsync(lineId);

            if (line == null)
                throw new EntityNotFoundException(typeof(CartLine), lineId);

            line.Amount = amount;
            await UpdateAsync(line);

            return line;
        }

        public async Task<CartLine> AddCartLine(int productId, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount should be higher than 0");

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new EntityNotFoundException(typeof(Product), productId);


            var matchingLines = await Repository.Where(cl => cl.ProductId == productId);
            var cartLine = matchingLines.FirstOrDefault();
            if (cartLine != null)
            {
                cartLine.Amount = cartLine.Amount + amount;
                await UpdateAsync(cartLine);
            }
            else
            {
                cartLine = new CartLine
                {
                    Product = product,
                    Amount = amount
                };

                await InsertAsync(cartLine);
            }
            return cartLine;
        }

        public async Task DeleteCartLine(int lineId)
        {
            var line = await Repository.GetByIdAsync(lineId);
            if (line == null)
                throw new EntityNotFoundException(typeof(CartLine), lineId);

            await DeleteAsync(line);
        }
    }

}
