using System;
using System.Collections.Generic;

namespace CSU_Core.Models
{
    public partial class Supplydocument
    {
        public decimal Documentid { get; set; }
        public string Documentname { get; set; } = null!;
        public string? Documentsubject { get; set; }
        public decimal? Createdby { get; set; }
        public decimal? Warehouseid { get; set; }
        public decimal? Itemid { get; set; }
        public DateTime? Createddatetime { get; set; }
        public string? Status { get; set; }

        public virtual User? CreatedbyNavigation { get; set; }
        public virtual Item? Item { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
