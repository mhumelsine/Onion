using Logistics.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Core.Stores
{
    public interface IInventoryStore
    {
        IEnumerable<InventoryMaster> GetAllMasterOnly();

        InventoryMaster GetMasterByID(int id);

        InventoryMaster GetMasterByLin(string lin);

        void Save(InventoryMaster model);

        void Delete(int id);
    }
}
