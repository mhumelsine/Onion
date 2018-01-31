using Isf.XCutting.Commands;
using Isf.XCutting.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Impl.Stores
{
    public abstract class EfStoreBase
    {
        private readonly DbContext context;
        protected readonly ILogger logger;

        protected LogisticsContext Context { get { return (LogisticsContext) context; } }

        public EfStoreBase(DbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public CommandResult Save()
        {
            var result = new CommandResult();

            try
            {
                context.SaveChanges();
                result.State = CommandState.Succeeded;
            }
            catch (Exception ex)
            {
                var error = "Error saving changes to the database";

                logger.Error(error, ex);

                result.AddError(error, CommandState.ExecutionFailed);
            }
            return result;
        }
    }
}
