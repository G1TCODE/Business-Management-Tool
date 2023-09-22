using BMT.Application.Orders.PlaceOrder;
using BMT.Domain.Abstractions;
using BMT.Domain.Orders.Events;
using BMT.Domain.Orders;
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
using BMT.Domain.Locations;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.ShoppingCart.CreateShoppingCart
{
    internal sealed class CreateShoppingCartCommandHandler : ICommandHandler<CreateShoppingCartCommand, Guid>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SP.IShoppingCartRepository _shoppingcartrepository;
        private readonly OrderPricingService _orderPricingService;
        private readonly IStoreRepository _storeRespository;

        public CreateShoppingCartCommandHandler
            (IManagerRepository managerRepository,
            IUnitOfWork unitOfWork,
            SP.IShoppingCartRepository shoppingCartRepository,
            OrderPricingService orderpricingservice,
            IStoreRepository storeRepository)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
            _shoppingcartrepository = shoppingCartRepository;
            _orderPricingService = orderpricingservice;
            _storeRespository = storeRepository;
        }

        public async Task<Result<Guid>> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            var store = await _storeRespository.GetByIdAsync(request.storeorwarehouseid, cancellationToken);

            if (manager is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            try
            {
                var shoppingcart = SP.ShoppingCart.CreateCart(store.Id, manager.Id, request.ordertype);

                _shoppingcartrepository.Add(shoppingcart);

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
