using BMT.Application.ShoppingCart.AddItem;
using BMT.Domain.Abstractions;
using BMT.Domain.Orders.Events;
using BMT.Domain.Orders;
using BMT.Domain.Orders.Services;
using BMT.Domain.Products;
using BMT.Domain.Users.UserErrors;
using BMT.Domain.Users;
using SP = BMT.Domain.Shopping_Cart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.ShoppingCart.RemoveItem
{
    internal sealed class RemoveItemCommandHandler : ICommandHandler<RemoveItemCommand, Guid>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly SP.IShoppingCartRepository _shoppingcartrepository;
        private readonly OrderPricingService _orderPricingService;
        private readonly IProductRepository _productRepository;

        public RemoveItemCommandHandler
            (IManagerRepository managerRepository,
            IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            SP.IShoppingCartRepository shoppingCartRepository,
            OrderPricingService orderpricingservice,
            IProductRepository productRepository)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _shoppingcartrepository = shoppingCartRepository;
            _orderPricingService = orderpricingservice;
            _productRepository = productRepository;
        }

        public async Task<Result<Guid>> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingcart = await _shoppingcartrepository.GetByIdAsync(request.shoppingcartid, cancellationToken);

            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            if (manager is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            try
            {
                SP.ShoppingCart.RemoveItem(shoppingcart, request.itemid);

                shoppingcart.ShoppingCartTotal = _orderPricingService.CalculateOrderPrice(shoppingcart.TheShoppingCart);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return shoppingcart.Id;
            }
            catch (DBConcurrencyException)
            {
                return Result.Failure<Guid>(OrderErrors.OrderNotPlaced);
            }
        }
    }
}
