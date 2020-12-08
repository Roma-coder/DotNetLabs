using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab7.Models
{
    public class ClientAndOrderKey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Count { get; set; }

        public List<ClientAndOrder> Orders {get; set;}
    }
}
