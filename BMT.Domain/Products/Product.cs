using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Abstractions;
using BMT.Domain.Shared;

namespace BMT.Domain.Products
{
    public class Product : Entity
    {

        #region Constructor

        public Product(Guid id) : base(id)
        {

        }
        public Product
            (Guid id,
            Name name,
            ProductCategory category,
            Money unitprice,
            bool inStock,
            float rating)
            : base(id)
        {
            Name = name;
            Category = category;
            UnitPrice = unitprice;
            InStock = inStock;
            Rating = rating;
        }

        #endregion Constructor

        #region Properties

        public Name Name { get; private set; }

        public ProductCategory Category { get; private set; }

        public Money UnitPrice { get; private set; }

        public bool InStock { get; private set; }

        public float Rating { get; private set; }

        #endregion Properties

    }
}
