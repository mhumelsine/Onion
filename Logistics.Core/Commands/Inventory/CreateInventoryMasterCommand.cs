using Isf.XCutting.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Isf.XCutting.Validations;
using Logistics.Core.DTOs;
using Logistics.Core.Stores;

namespace Logistics.Core.Commands.Inventory
{
    public class CreateInventoryMasterCommand : Command
    {
        private readonly InventoryMaster inventoryMaster;
        private readonly IInventoryStore inventoryStore;

        public CreateInventoryMasterCommand(InventoryMaster inventoryMaster, IInventoryStore inventoryStore)
        {
            this.inventoryMaster = inventoryMaster;
            this.inventoryStore = inventoryStore;
        }

        public override IEnumerable<ValidationError> Validate()
        {
            var existingInventoryItem = inventoryStore.GetMasterByLin(inventoryMaster.LIN);

            if (existingInventoryItem != null)
            {
                var property = nameof(inventoryMaster.LIN);

                yield return new ValidationError(property,
                    string.Format("LIN: {0} is already is use by: {1}", property, inventoryMaster.GeneralNomenclature));
            }
        }

        public override void Execute()
        {
            inventoryStore.Save(inventoryMaster);
        }
    }
}
