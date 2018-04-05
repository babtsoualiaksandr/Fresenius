using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public string  Num { get; set; }
        public string  Date { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }


        public ICollection<InvoiceSparepart> InvoiceSpareparts { get; set; }



    }
}
