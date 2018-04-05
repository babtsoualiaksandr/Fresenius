using Fresenius.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.ViewsModel
{
    public class ViewModelForInvoceToSpareparts
    {
        public IEnumerable<Invoice> Invoices  { get; set; }
        public IEnumerable<Sparepart>    Spareparts { get; set; }
        public IEnumerable<InvoiceSparepart> InvoiceSpareparts { get; set; }
    }
}
