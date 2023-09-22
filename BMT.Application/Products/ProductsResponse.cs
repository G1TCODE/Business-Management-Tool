using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Products
{
    public sealed class ProductResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public int Category { get; private set; }

        public int UnitPrice { get; private set; }

        public bool InStock { get; private set; }

        public float Rating { get; private set; }
    }
}
