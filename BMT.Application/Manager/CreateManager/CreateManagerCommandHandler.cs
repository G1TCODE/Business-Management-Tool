using BMT.Domain.Abstractions;
using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerNamespace = BMT.Domain.Users;
using BMT.Application.Abstractions.Messaging;

namespace BMT.Application.Manager.CreateManager
{
    internal class CreateManagerCommandHandler : ICommandHandler<CreateManagerCommand, Guid>
    {
        private readonly ManagerNamespace.IManagerRepository _managerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateManagerCommandHandler(
            ManagerNamespace.IManagerRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _managerRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var newHireManager = ManagerNamespace.Manager.CreateManager(
                Guid.NewGuid(),
                new Name(request.newHireName),
                new Password(request.newHirePassword),
                new Email(request.newHireEmail),
                request.newHireScope,
                request.newHireStoreId);

            _managerRepository.Add(newHireManager);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newHireManager.Id;
        }
    }
}
