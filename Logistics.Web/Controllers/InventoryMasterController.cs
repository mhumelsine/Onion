using Isf.XCutting.Commands;
using Logistics.Core.Commands.Inventory;
using Logistics.Core.DTOs;
using Logistics.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Web.Controllers
{
    public class InventoryMasterController : Controller
    {
        private readonly CommandDispatcher dispatcher;
        private readonly IInventoryStore inventoryStore;

        public InventoryMasterController(CommandDispatcher dispatcher, IInventoryStore inventoryStore)
        {
            this.dispatcher = dispatcher;
            this.inventoryStore = inventoryStore;
        }

        [HttpPost]
        public IActionResult Post([FromBody]InventoryMaster inventoryMaster)
        {
            var command = new CreateInventoryMasterCommand(inventoryMaster, inventoryStore);

            var result = dispatcher.Execute(command);

            if (result.State == CommandState.Succeeded)
            {
                var uri = Url.Action("Get", new { id = inventoryMaster.Id });
                return Created(uri, inventoryMaster);
            }

            return BadRequest(result.ErrorDictionary);
        }
    }
}
