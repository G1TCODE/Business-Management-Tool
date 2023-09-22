using BMT.Application.ShoppingCart.CreateShoppingCart;
using BMT.Domain.Abstractions;
using BMT.Domain.Locations;
using BMT.Domain.Orders.Events;
using BMT.Domain.Orders.Services;
using BMT.Domain.Users.UserErrors;
using BMT.Domain.Users;
using SP = BMT.Domain.Shopping_Cart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Products;
using BMT.Domain.Orders;
using MediatR.NotificationPublishers;
using BMT.Application.Abstractions.Messaging;
using BMT.Application.ShoppingCart.RemoveItem;

namespace BMT.Application.ShoppingCart.IncreaseQuantity
{
    internal sealed class IncreaseItemQuantityByCommandHandler : ICommandHandler<IncreaseItemQuantityByCommand, Guid>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SP.IShoppingCartRepository _shoppingcartrepository;
        private readonly OrderPricingService _orderPricingService;
        private readonly IStoreRepository _storeRespository;
        private readonly IProductRepository _productRepository;

        public IncreaseItemQuantityByCommandHandler
            (IManagerRepository managerRepository,
            IUnitOfWork unitOfWork,
            SP.IShoppingCartRepository shoppingCartRepository,
            OrderPricingService orderpricingservice,
            IStoreRepository storeRepository,
            IProductRepository productRepository)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
            _shoppingcartrepository = shoppingCartRepository;
            _orderPricingService = orderpricingservice;
            _storeRespository = storeRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<Guid>> Handle(IncreaseItemQuantityByCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            var shoppingcart = await _shoppingcartrepository.GetByIdAsync(request.shoppingcartid, cancellationToken);

            if (manager is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            try
            {
                SP.ShoppingCart.IncreaseItemQuantity(shoppingcart, manager, request.itemid, request.increaseBy);

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
