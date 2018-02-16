using Isf.XCutting.Commands;
using Isf.XCutting.Transactions;
using Logistics.Impl.Stores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Web.Controllers
{
    public class ControllerBase : Controller
    {
        private CommandRunner executor;

        public ControllerBase(CommandRunner executor)
        {
            this.executor = executor;
        }

        protected void Execute(Command command)
        {
            executor.Execute(command);
        }
    }
}
