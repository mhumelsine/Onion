using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Logistics.Core.DTOs
{
    public class InventoryMaster
    {
        [Key]
        public int Id { get; set; }
        public string LIN { get; set; }

        public string GeneralNomenclature { get; set; }

        public bool IsGArmy { get; set; }

        public int TrackingType { get; set; }

        public int Status { get; set; }
    }
}
