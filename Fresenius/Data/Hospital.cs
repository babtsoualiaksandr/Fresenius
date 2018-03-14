using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Data
{
    public class Hospital
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Название УЗ")]
        public string Nam { get; set; }
        [Display(Name = "Широта")]
        public double latitude { get; set; }
        [Display(Name = "Долгота")]
        public double longitude { get; set; }
    }
}
