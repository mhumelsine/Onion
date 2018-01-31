using Logistics.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Impl.Stores
{
    public class LogisticsContext : DbContext
    {
        public LogisticsContext(DbContextOptions<LogisticsContext> options)
            : base(options)
        {

        }

        public DbSet<InventoryMaster> InventoryMasters { get; set; }
    }
}
