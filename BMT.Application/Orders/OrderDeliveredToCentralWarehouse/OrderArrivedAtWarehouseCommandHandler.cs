using BMT.Domain.Abstractions;
using BMT.Domain.Locations;
using BMT.Domain.Orders;
using BMT.Domain.Users.UserErrors;
using ManagerNamespace = BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Application.Abstractions.DataLink;
using BMT.Application.Manager.CreateManager;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.Orders.OrderDeliveredToCentralWarehouse
{
    internal sealed class OrderArrivedAtWarehouseCommandHandler : ICommandHandler<OrderArrivedAtWarehouseCommand>
    {
        private readonly ManagerNamespace.IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IStoreRepository _storeRepository;

        public OrderArrivedAtWarehouseCommandHandler
            (ManagerNamespace.IManagerRepository managerRepository,
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
            OrderArrivedAtWarehouseCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.orderid, cancellationToken);

            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            var store = await _storeRepository.GetByIdAsync(request.storeid, cancellationToken);

            if (manager is null)
            {
                return Result.ActionFailed(ManagerErrors.ManagerDoesNotExist);
            }

            var result = ManagerNamespace.Manager.MarkAsDeliverdToCentralWarehouse(manager, order, store);

            if (result.ResultFailed)
            {
                return result;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.ActionSuccessful();
        }
    }
}