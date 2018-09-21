using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface ICartLineService : IService<CartLine>
    {
        Task<CartLine> UpdateCartLine(int lineId, int amount);

        Task<CartLine> AddCartLine(int productId, int amount);

        Task DeleteCartLine(int lineId);
    }
}
