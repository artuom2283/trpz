using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusServ.DTOs
{
    public class BusDto
    {
        public int Id { get; set; }
        public string BusNumber { get; set; }
        public int Capacity { get; set; }
    }
}