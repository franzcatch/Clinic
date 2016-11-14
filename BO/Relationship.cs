using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Relationship : BusinessBase
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}