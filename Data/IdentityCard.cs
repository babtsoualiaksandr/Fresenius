using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class IdentityCard
    {
        public int Id { get; set; }
        [Display(Name = "Номер регестрационного удостоверения")]
        public string Number { get; set; }
        [Display(Name = "Дата регистрации")]
        public DateTime DateOfRegistration { get; set; }
        [Display(Name = "Срок действия")]
        public DateTime  Expiration { get; set; }
        [Display(Name = "Заявитель")]
        public string Applicant { get; set; }
        [Display(Name = "Для чего это всё нужно :))")]
        public string Purpose { get; set; }
    }
}
