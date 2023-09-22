using BMT.Application.Orders.PlaceOrder;
using BMT.Domain.Abstractions;
using BMT.Domain.Locations;
using BMT.Domain.Orders.Events;
using BMT.Domain.Orders;
using OrderRepo = BMT.Domain.Orders;
using BMT.Domain.Products;
using BMT.Domain.Shopping_Cart;
using BMT.Domain.Users;
using BMT.Domain.Users.UserErrors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Application.Abstractions.Messaging;



namespace BMT.Application.Orders.CancelOrder
{
    internal sealed class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IStoreRepository _storeRepository;

        public CancelOrderCommandHandler
            (IManagerRepository managerRepository,
            IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            IStoreRepository storeRepository)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _storeRepository = storeRepository;
        }

        public async Task<Result> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.orderid, cancellationToken);

            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            var storeorwarehouseid = await _storeRepository.GetByIdAsync(request.storeorwarehouseid, cancellationToken);

            if (manager is null)
            {
                return Result.ActionFailed(ManagerErrors.ManagerDoesNotExist);
            }

            var result = manager.MarkAsCancelled(manager, order, storeorwarehouseid);

            if (result.ResultFailed)
            {
                return result;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.ActionSuccessful();
        }
    }
}