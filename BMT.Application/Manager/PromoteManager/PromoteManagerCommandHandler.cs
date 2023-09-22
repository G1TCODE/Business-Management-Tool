using BMT.Application.Orders.ApproveOrder;
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
using MediatR;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.Manager.PromoteManager
{
    internal sealed class PromoteManagerCommandHandler : ICommandHandler<PromoteManagerCommand, Guid>
    {
        private readonly ManagerNamespace.IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PromoteManagerCommandHandler
            (ManagerNamespace.IManagerRepository managerRepository,
            IUnitOfWork unitOfWork)
        {
            _managerRepository = managerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(PromoteManagerCommand request, CancellationToken cancellationToken)
        {
            var bossManager = await _managerRepository.GetByIdAsync(request.bossId, cancellationToken);

            var managerToPromote = await _managerRepository.GetByIdAsync(request.toPromoteId, cancellationToken);

            if (bossManager is null || managerToPromote is null)
            {
                return Result.Failure<Guid>(ManagerErrors.ManagerDoesNotExist);
            }

            var result = ManagerNamespace.Manager.PromoteManager(bossManager, managerToPromote);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return managerToPromote.Id;
        }
    }
}
