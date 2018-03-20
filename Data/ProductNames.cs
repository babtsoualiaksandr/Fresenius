using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class ProductNames
    {
        [Display(Name = "Номер")]
        public int Item { get; set; }
        [Display(Name = "Наименование")]
        public string  Designation { get; set; }
        [Display(Name = "Код, артикул, ГОСТ, ТУ, НТД, модель")]
        public string  Gost { get; set; }
        [Display(Name = "Производитель")]
        public string Manufacturer { get; set; }
        [Display(Name = "Рег. номер товара")]
        public string RegItem { get; set; }
        [Display(Name = "Тип")]
        public string  Type { get; set; }  
    }
}
