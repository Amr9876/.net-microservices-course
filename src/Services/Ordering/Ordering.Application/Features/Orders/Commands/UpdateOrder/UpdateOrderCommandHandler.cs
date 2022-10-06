
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _repository.GetByIdAsync(request.Id);

        if (orderToUpdate is null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

        await _repository.UpdateAsync(orderToUpdate);

        _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

        return Unit.Value;
    }
}
