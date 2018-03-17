using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class Equipment
    {
        public int Id { get; set; }
        [Display(Name = "Код Оборудования")]
        public string Code { get; set; }
        [Display(Name = "Наименование ")]
        public string Name { get; set; }
        [Display(Name = "Описание ")]
        public string Description { get; set; }
        [Display(Name = "Фото ")]
        public string Image { get; set; }
        [Display(Name = "Номер регистрационного удостоверения")]
        public string RegNumber { get; set; }
        [Display(Name = "Примечание")]
        public DateTime Ps { get; set; }         
    }
}
