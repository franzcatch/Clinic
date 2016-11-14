using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Household : BusinessBase
    {
        public List<Person> People { get; set; }
        public Insurance Insurance { get; set; }
    }
}