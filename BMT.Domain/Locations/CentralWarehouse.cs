using BMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Abstractions;
using BMT.Domain.Orders;
using BMT.Domain.Products;

namespace BMT.Domain.Locations
{
    public class CentralWarehouse : Entity
    {

        #region Constructor

        public CentralWarehouse
            (Guid id,
            Name name,
            Address address,
            string openingdate,
            Guid sitemanagerid)
            : base(id)
        {
            Name = name;
            Address = address;
            OpeningDate = openingdate;
            ManagerId = sitemanagerid;
        }

        #endregion Constructor

        #region Collections
        public List<Item> InventoryOnOrder { get; private set; } = new();
        public List<Item> InventoryOnHand { get; private set; } = new();

        #endregion Collections

        //TODO Add methods

        #region Methods

        #endregion Methods

        #region Properties

        public Name Name { get; private set; }

        public Guid ManagerId { get; private set; }

        public string OpeningDate { get; private set; }

        public Address Address { get; private set; }

        #endregion Properties

    }
}

