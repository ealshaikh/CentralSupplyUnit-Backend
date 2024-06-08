using System;
using System.Collections.Generic;

namespace CSU_Core.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Items = new HashSet<Item>();
            Supplydocuments = new HashSet<Supplydocument>();
        }

        public decimal Warehouseid { get; set; }
        public string Warehousename { get; set; } = null!;
        public string? Warehousedescription { get; set; }
        public decimal? Createdby { get; set; }
        public DateTime? Createddatetime { get; set; }

        public virtual User? CreatedbyNavigation { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Supplydocument> Supplydocuments { get; set; }
    }
}
