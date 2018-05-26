using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.ViewsModel
{
    public class VieweModelForEquipmentsIC
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
        [Display(Name = "Дата ")] 
        public string Ps { get; set; }

        [Display(Name = "Номер регестрационного удостоверения")]
        public string Number { get; set; }
        [Display(Name = "Дата регистрации")]
        public DateTime DateOfRegistration { get; set; }
        [Display(Name = "Срок действия")]
        public DateTime Expiration { get; set; }
        [Display(Name = "Заявитель")]
        public string Applicant { get; set; }
        [Display(Name = "Для чего это всё нужно :))")]
        public string Purpose { get; set; }

    }
}
