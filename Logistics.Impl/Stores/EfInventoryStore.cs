using Isf.XCutting.Logging;
using Logistics.Core.DTOs;
using Logistics.Core.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logistics.Impl.Stores
{
    public class EfInventoryStore : EfStoreBase, IInventoryStore
    {
        public EfInventoryStore(LogisticsContext context, ILogger logger) : base(context, logger)
        {

            if (this.Context.InventoryMasters.Count() == 0)
            {
                for (int x = 1; x < 10; x++)
                {
                    this.Context.InventoryMasters.Add(new InventoryMaster
                    {
                        Id = x,
                        LIN = "Lin " + x.ToString(),
                        Status = 1,
                        GeneralNomenclature = "Nomenclature " + x.ToString(),
                        TrackingType = 1,
                        IsGArmy = true
                    });
                }
                this.Context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            InventoryMaster _modelToBeDeleted = GetMasterByID(id);

            // Check to make sure we found the ID
            if (_modelToBeDeleted == null) throw new ApplicationException(string.Format("Deleted Failed. Could not find Inventory Master ID {0}", id));

            this.Context.Remove(_modelToBeDeleted);
            this.Context.SaveChanges();
        }

        public IEnumerable<InventoryMaster> GetAllMasterOnly()
        {
            return this.Context.InventoryMasters.ToList();
        }

        public InventoryMaster GetMasterByID(int id)
        {
            return this.Context.InventoryMasters.SingleOrDefault(x => x.Id == id);
        }

        public InventoryMaster GetMasterByLin(string lin)
        {
            return this.Context.InventoryMasters.SingleOrDefault(x => x.LIN == lin);
        }

        public void Save(InventoryMaster model)
        {
            if (model.Id > 0)
            {
                this.Context.InventoryMasters.Update(model);
            }
            else
            {
                // Temp to get the max ID. InMemory
                int _max = this.Context.InventoryMasters.Max(x => x.Id) + 1;
                model.Id = _max;
                this.Context.InventoryMasters.Add(model);
            }
            this.Context.SaveChanges();
        }
    }
}
