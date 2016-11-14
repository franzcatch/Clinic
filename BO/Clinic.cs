using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Clinic : Entity
    {
        public string Name {
            get
            {
                return this.Name1;
            }
            set
            {
                this.Name1 = value;
            }
        }
        public List<Provider> Providers { get; set; }
        public List<Service> Services { get; set; }
        public List<Room> Rooms { get; set; }
    }
}