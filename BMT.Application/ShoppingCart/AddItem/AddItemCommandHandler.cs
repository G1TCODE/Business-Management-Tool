using BMT.Application.Orders.PlaceOrder;
using BMT.Domain.Abstractions;
using BMT.Domain.Orders.Events;
using OrderRepo = BMT.Domain.Orders;
using BMT.Domain.Orders.Services;
using BMT.Domain.Orders;
using SP = BMT.Domain.Shopping_Cart;
using BMT.Domain.Users.UserErrors;
using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Products;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.ShoppingCart.AddItem
{
    internal sealed class AddItemCommandHandler : ICommandHandler<AddItemCommand, Guid>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly SP.IShoppingCartRepository _shoppingcartrepository;
        private readonly OrderPricingService _orderPricingService;
        private readonly IProductRepository _productRepository;

        public AddItemCommandHandler
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

        public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var shoppingcart = await _shoppingcartrepository.GetByIdAsync(request.shoppingcartid, cancellationToken);

            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            var product = await _productRepository.GetByIdAsync(request.productid, cancellationToken);

            if (manager is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            try
            {
                SP.ShoppingCart.AddItem(shoppingcart, product, manager);

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
