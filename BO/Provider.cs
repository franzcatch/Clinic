using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Provider : Entity
    {
        public Clinic Clinic { get; set; }
    }
}