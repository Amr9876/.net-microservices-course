
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var ordersList = await _db.Orders.Where(x => x.UserName == userName).ToListAsync();

        return ordersList;
    }
}
