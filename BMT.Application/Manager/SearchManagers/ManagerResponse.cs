using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Manager.SearchManagers
{
    public sealed class ManagerResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public int Level { get; init; }

        public string Password { get; init; }

        public string Email { get; private set; }

        public int ManagerScope { get; init; }

        public Guid StoreOrWarehouseId { get; init; }
    }
}
