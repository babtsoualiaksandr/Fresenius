using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class InvoiceSparepart
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice  Invoice { get; set; }

        public int SparepartId { get; set; }
        public Sparepart   Sparepart { get; set; }
    }
}
