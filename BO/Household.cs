using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Household : BusinessBase
    {
        public Person Payer
        {
            get
            {
                return People.SingleOrDefault(x => x.IsPayer == true);
            }
        }
        public List<Person> People { get; set; }
        public string InsuranceName { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
    }
}