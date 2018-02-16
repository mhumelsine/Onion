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

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
