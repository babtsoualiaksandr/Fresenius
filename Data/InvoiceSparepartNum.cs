using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class InvoiceSparepartNum
    {
        public int Id { get; set; }
        public int InvoiceNumId { get; set; }
        public InvoiceNum InvoiceNum { get; set; }

        public  string Num { get; set; }

        public int SparepartId { get; set; }
        public Sparepart Sparepart { get; set; }
    }
}

