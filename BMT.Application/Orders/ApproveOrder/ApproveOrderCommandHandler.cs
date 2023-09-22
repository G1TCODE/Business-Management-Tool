using BMT.Application.Orders.CancelOrder;
using BMT.Domain.Abstractions;
using BMT.Domain.Locations;
using OrderRepo = BMT.Domain.Orders;
using BMT.Domain.Orders;
using BMT.Domain.Users.UserErrors;
using BMT.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.Orders.ApproveOrder
{
    internal class ApproveOrderCommandHandler : ICommandHandler<ApproveOrderCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IStoreRepository _storeRepository;

        public ApproveOrderCommandHandler
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

        public async Task<Result> Handle(ApproveOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.orderid, cancellationToken);

            var manager = await _managerRepository.GetByIdAsync(request.managerid, cancellationToken);

            var store = await _storeRepository.GetByIdAsync(request.storeid, cancellationToken);;

            if (manager is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            var result = manager.MarkAsApproved(manager, order, store);

            if(result.ResultFailed)
            {
                return result;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.ActionSuccessful();
        }
    }
}
