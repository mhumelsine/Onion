using Isf.XCutting.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Isf.XCutting.Validations;
using Logistics.Core.DTOs;
using Logistics.Core.Stores;

namespace Logistics.Core.Commands.Inventory
{
    public class CreateInventoryMasterCommand : CommandBase
    {
        private readonly InventoryMaster inventoryMaster;
        private readonly IInventoryStore inventoryStore;

        public CreateInventoryMasterCommand(InventoryMaster inventoryMaster, IInventoryStore inventoryStore)
        {
            this.inventoryMaster = inventoryMaster;
            this.inventoryStore = inventoryStore;
        }
        public override ObjectValidationResult PreValidate()
        {
            var existingInventoryItem = inventoryStore.GetMasterByLin(inventoryMaster.LIN);

            if(existingInventoryItem != null)
            {
                return ObjectValidationResult.Create(
                    string.Format("LIN: {0} is already is use by: {1}", inventoryMaster.LIN, inventoryMaster.GeneralNomenclature),
                    "LIN");
            }

            return ObjectValidationResult.OK;
        }
        public override CommandResult Execute()
        {
            inventoryStore.Save(inventoryMaster);
            return CommandResult.OK;
        }
    }
}
