using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class Sparepart
    {
        public int Id { get; set; }
        [Display(Name = "Номер запчасти")]
        public string Number { get; set; }
        [Display(Name = "Наименование En.")]
        public string NameEn { get; set; }
        [Display(Name = "Наименование Rus. ")]
        public string NameRus { get; set; }

        
        public int? EquipmentId { get; set; }
        [Display(Name = "Оборуд. ")]
        public Equipment Equipment { get; set; }

        [Display(Name = "Краткое_Описание_Запчасти")]
        public string Description { get; set; }


        public int? ManufacturerId { get; set; }
        [Display(Name = "Производитель")]
        public Manufacturer Manufacturer { get; set; }

        
        public int? CountryId { get; set; }
        [Display(Name = "Страна")]
        public Country Country { get; set; }

        [Display(Name = "Фото ")]
        public string Image { get; set; }

        public bool Mark { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return NameEn + " / " + NameRus ;
            }
        }

        public ICollection<InvoiceSparepart>    InvoiceSpareparts { get; set; }
        public ICollection<InvoiceSparepartNum> InvoiceSparepartNums { get; set; }



    }
}
