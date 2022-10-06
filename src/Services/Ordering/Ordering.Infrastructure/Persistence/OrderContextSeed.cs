using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
    {
        if (!context.Orders.Any())
        {
            context.Orders.AddRange(GetPreconfiguredOrders());
            await context.SaveChangesAsync();
            logger.LogInformation("Seed Dataabase associated with context {0}", typeof(OrderContext).Name);
        }
    }

    public static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order() { UserName = "John Doe", FirstName = "John", LastName = "Doe", EmailAddress = "amr.aig.2007@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
        };
    }
}
