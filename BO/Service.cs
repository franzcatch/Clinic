using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Service : BusinessBase
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Minutes { get; set; }
    }
}