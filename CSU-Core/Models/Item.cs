using System;
using System.Collections.Generic;

namespace CSU_Core.Models
{
    public partial class Item
    {
        public Item()
        {
            Supplydocuments = new HashSet<Supplydocument>();
        }

        public decimal Itemid { get; set; }
        public string Itemname { get; set; } = null!;
        public string? Itemdescription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Warehouseid { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual ICollection<Supplydocument> Supplydocuments { get; set; }
    }
}
