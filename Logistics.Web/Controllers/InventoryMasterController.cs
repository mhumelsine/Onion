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
    [Route("[controller]")]
    public class InventoryMasterController : ControllerBase
    {
        private readonly IInventoryStore inventoryStore;

        public InventoryMasterController(IInventoryStore inventoryStore, CommandRunner executor) : base (executor)
        {
            this.inventoryStore = inventoryStore;
        }

        [HttpPost]
        public IActionResult Post([FromBody]InventoryMaster inventoryMaster)
        {
            var command = new CreateInventoryMasterCommand(inventoryMaster, inventoryStore);

            Execute(command);

            if (command.State == CommandState.Succeeded)
            {
                var uri = Url.Action("Get", new { id = inventoryMaster.Id });
                return Created(uri, inventoryMaster);
            }

            return BadRequest(command.ErrorDictionary);
        }
    }
}
