using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Оборудование ")]
        public int? EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Производитель")]
        public int? ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Display(Name = "Страна происхождения")]
        public int? CountryId { get; set; }                                                               
        public Country Country { get; set; }

        [Display(Name = "Фото ")]
        public string Image { get; set; }

    }
}
