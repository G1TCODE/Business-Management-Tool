using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Abstractions;
using BMT.Domain.Users;
using BMT.Domain.Products;
using OrderRepo = BMT.Domain.Orders;
using BMT.Domain.Orders.Services;
using BMT.Domain.Locations;
using BMT.Domain.Users.UserErrors;
using BMT.Domain.Orders;
using BMT.Domain.Shopping_Cart;
using System.Data;
using BMT.Domain.Orders.Events;

namespace BMT.Application.Orders.PlaceOrder
{
    internal sealed class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand, Guid>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingcartrepository;
        private readonly OrderPricingService _orderPricingService;

        public PlaceOrderCommandHandler
            (IManagerRepository managerRepository, 
            IUnitOfWork unitOfWork, 
            IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository,
            OrderPricingService orderpricingservice)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _shoppingcartrepository = shoppingCartRepository;
            _orderPricingService = orderpricingservice;
        }

        public async Task<Result<Guid>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var shoppingcart = await _shoppingcartrepository.GetByIdAsync(request.shoppingcartid, cancellationToken);

            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            if (manager is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            try
            {
                var order = Order.CreateOrder(shoppingcart);

                order.TotalCost = _orderPricingService.CalculateOrderPrice(order.OrderItems);

                _orderRepository.Add(order);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return order.Id;
            }
            catch (DBConcurrencyException)
            {
                return Result.Failure<Guid>(OrderErrors.OrderNotPlaced);
            }
        }
    }
}
