using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fresenius.Models
{
    
    public class DataChart
    {
     
        public string name { get; set; }
        public string style { get; set; }
        public List<Items> items  { get; set; }


    }

    public class Items
    {
   
        public double[] center { get; set; }
        public string name { get; set; }
       
    }




}
