using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab7.Models
{
    public class ClientAndOrder
    {
        public string Option { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
