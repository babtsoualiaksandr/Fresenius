using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class InvoiceNum
    {
        public int Id { get; set; }
        [Display(Name = "Номер")]
        public string Num { get; set; }
        [Display(Name = "Дата")]
        public string Date { get; set; }
        [Display(Name = "Отправитель")]
        public string Sender { get; set; }
        [Display(Name = "Получатель")]
        public string Recipient { get; set; }


        public ICollection<InvoiceSparepartNum> InvoiceSparepartNums { get; set; }
    }
}
